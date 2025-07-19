using IndentRainbow.Extension.Drawing;
using IndentRainbow.Extension.Options;
using IndentRainbow.Extension.Options.Model;
using IndentRainbow.Logic.Classification;
using IndentRainbow.Logic.Colors;
using IndentRainbow.Logic.Drawing;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using Microsoft.VisualStudio.TextManager.Interop;

namespace IndentRainbow.Extension
{
    /// <summary>
    /// Decorates the text using all necessary components
    /// </summary>
    public sealed class Indent
    {
        /// <summary>
        /// The layer of the adornment.
        /// </summary>
        private readonly IAdornmentLayer layer;

        /// <summary>
        /// Text view where the adornment is created.
        /// </summary>
        private readonly IWpfTextView view;

        /// <summary>
        /// Background drawer which is used for drawing the rainbow effect
        /// </summary>
        private readonly IBackgroundTextIndexDrawer drawer;

        /// <summary>
        /// Color getter used for getting the correct color for an indentation level
        /// </summary>
        private readonly IRainbowBrushGetter colorGetter;

        /// <summary>
        /// Decorator used for decorating a line
        /// </summary>
        private readonly ILineDecorator decorator;

        private readonly IndentationCalculator indentationCalculator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Indent"/> class.
        /// </summary>
        /// <param name="view">Text view to create the adornment for</param>
        //Ignoring warning since this adornment is always on UI thread
#pragma warning disable VSTHRD010
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Not necessary")]
        public Indent(IWpfTextView view, IIndentationManagerService indentationManagerService)
        {
            if (view == null)
            {
                return;
            }
            this.layer = view.GetAdornmentLayer("Indent");
            this.view = view;
            this.view.LayoutChanged += OnLayoutChanged;
            drawer = new BackgroundTextIndexDrawer(this.layer, this.view);

            this.indentationCalculator = new IndentationCalculator(OptionsManager.indentationSizeMode.Get(), indentationManagerService, GetPath(view), this.view.TextBuffer);

            colorGetter = new RainbowBrushGetter(OptionsManager.colors.Get(), OptionsManager.errorBrush.Get(), OptionsManager.colorMode.Get(), OptionsManager.fadeColors.Get());

            var highlightingMode = OptionsManager.highlightingMode.Get();
            if (OptionsManager.highlightingMode.Get() == HighlightingMode.Monocolor)
            {
                decorator = new MonocolorLineDecorator(
                    drawer, colorGetter, indentationCalculator.indentValidator)
                {
                    detectErrors = OptionsManager.detectErrors.Get()
                };
            }
            else
            {
                decorator = new AlternatingLineDecorator(
                    drawer, colorGetter, indentationCalculator.indentValidator)
                {
                    detectErrors = OptionsManager.detectErrors.Get()
                };
            }
        }

        /// <summary>
        /// Handles whenever the text displayed in the view changes by adding the adornment to any reformatted lines
        /// </summary>
        /// <remarks><para>This event is raised whenever the rendered text displayed in the <see cref="ITextView"/> changes.</para>
        /// <para>It is raised whenever the view does a layout (which happens when DisplayTextLineContainingBufferPosition is called or in response to text or classification changes).</para>
        /// <para>It is also raised whenever the view scrolls horizontally or when its size changes.</para>
        /// </remarks>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        internal void OnLayoutChanged(object sender, TextViewLayoutChangedEventArgs e)
        {
            indentationCalculator.ReloadIndentationSize(this.view.TextBuffer);
            foreach (ITextViewLine line in e.NewOrReformattedLines)
            {
                CreateVisuals(line);
            }
        }

        /// <summary>
        /// Retrieves relevant information to pass it to the line decorator
        /// </summary>
        /// <param name="line">Line to add the adornments</param>
        private void CreateVisuals(ITextViewLine line)
        {
            int start = line.Start;
            int length = line.End - start;

            string text = line.Snapshot.GetText(start, length);
            decorator.DecorateLine(text, start);
        }

        private static string GetPath(IWpfTextView textView)
        {
            textView.TextBuffer.Properties.TryGetProperty(typeof(IVsTextBuffer), out IVsTextBuffer bufferAdapter);

            if (!(bufferAdapter is IPersistFileFormat persistFileFormat))
            {
                return null;
            }
            persistFileFormat.GetCurFile(out string filePath, out _);
            return filePath;
        }
    }
}
