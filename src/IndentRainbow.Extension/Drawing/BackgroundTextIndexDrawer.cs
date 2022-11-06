using System.Windows.Controls;
using System.Windows.Media;
using IndentRainbow.Logic.Drawing;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

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
            brush = new SolidColorBrush(Color.FromArgb(0x20, 0x00, 0x00, 0xff));
            brush.Freeze();
        }

        public void DrawBackground(int firstIndex, int length, Brush drawBrush)
        {
            var span = new SnapshotSpan(view.TextSnapshot, Span.FromBounds(firstIndex, firstIndex + length));
            Geometry geometry = view.TextViewLines.GetMarkerGeometry(span, false, new Thickness(0, 0, 0, view.LineHeight - view.TextViewLines.WpfTextViewLines[0].TextHeight));
            if (geometry != null)
            {
                var newRect = new Rect()
                {
                    X = geometry.Bounds.X,
                    Y = geometry.Bounds.Y,
                    Width = geometry.Bounds.Width,
                    Height = view.LineHeight
                };
                var copiedGeometry = new RectangleGeometry(newRect);
                var drawing = new GeometryDrawing(drawBrush, null, copiedGeometry);
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
                layer.AddAdornment(AdornmentPositioningBehavior.TextRelative, span, null, image, null);
            }

        }
    }
}
