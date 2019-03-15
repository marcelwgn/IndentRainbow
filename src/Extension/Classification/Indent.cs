using IndentRainbow.Logic.Classification;
using IndentRainbow.Logic.Drawing;
using IndentRainbow.Logic.Colors;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using System;
using IndentRainbow.Extension.Drawing;

namespace IndentRainbow.Extension
{
    /// <summary>
    /// Indent places red boxes behind all the "a"s in the editor window
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
        /// Validator used for checking wether a given string is a valid indentation
        /// </summary>
        private readonly IIndentValidator validator;

        /// <summary>
        /// Decorator used for decorating a line
        /// </summary>
        private readonly ILineDecorator decorator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Indent"/> class.
        /// </summary>
        /// <param name="view">Text view to create the adornment for</param>
        public Indent(IWpfTextView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            this.layer = view.GetAdornmentLayer("Indent");

            this.view = view;
            this.view.LayoutChanged += this.OnLayoutChanged;
            this.drawer = new BackgroundTextIndexDrawer(this.layer, this.view);
            this.colorGetter = new RainbowBrushGetter();
            this.validator = new IndentValidator();
            this.decorator = new LineDecorator(this.drawer, this.colorGetter, this.validator);
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
            foreach (ITextViewLine line in e.NewOrReformattedLines)
            {
                this.CreateVisuals(line);
            }
        }

        /// <summary>
        /// Retrieves relevant information to pass it to the line decorator
        /// </summary>
        /// <param name="line">Line to add the adornments</param>
        private void CreateVisuals(ITextViewLine line)
        {
            IWpfTextViewLineCollection textViewLines = this.view.TextViewLines;

            int start = line.Start;
            int end = line.End;

            string text = line.Snapshot.GetText();
            this.decorator.DecorateLine(text, start, end);
        }
    }
}
