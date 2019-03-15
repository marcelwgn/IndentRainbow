using IndentRainbow.Logic.Drawing;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using System.Windows.Controls;
using System.Windows.Media;

namespace IndentRainbow.Extension.Drawing
{
    public class BackgroundTextIndexDrawer : IBackgroundTextIndexDrawer
    {

        private readonly SolidColorBrush brush;

        private readonly IAdornmentLayer layer;
        private readonly IWpfTextView view;

        public BackgroundTextIndexDrawer(IAdornmentLayer layer, IWpfTextView view)
        {
            this.layer = layer;
            this.view = view;
            this.brush = new SolidColorBrush(Color.FromArgb(0x20, 0x00, 0x00, 0xff));
            this.brush.Freeze();
        }

        public void DrawBackground(int firstIndex, int length, Brush drawBrush)
        {
            SnapshotSpan span = new SnapshotSpan(this.view.TextSnapshot, Span.FromBounds(firstIndex, firstIndex + length));
            Geometry geometry = this.view.TextViewLines.GetMarkerGeometry(span);
            if (geometry != null)
            {
                var drawing = new GeometryDrawing(drawBrush, null, geometry);
                drawing.Freeze();

                var drawingImage = new DrawingImage(drawing);
                drawingImage.Freeze();

                var image = new Image
                {
                    Source = drawingImage,
                };

                // Align the image with the top of the bounds of the text geometry
                Canvas.SetLeft(image, geometry.Bounds.Left);
                Canvas.SetTop(image, geometry.Bounds.Top);
                this.layer.AddAdornment(AdornmentPositioningBehavior.TextRelative, span, null, image, null);
            }

        }
    }
}
