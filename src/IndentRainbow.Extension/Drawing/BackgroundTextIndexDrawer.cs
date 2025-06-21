using System;
using System.Windows;
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

        public void DrawBackground(int firstIndex, int length, Brush drawBrush, int indexTextLine)
        {
            var span = new SnapshotSpan(view.TextSnapshot, Span.FromBounds(firstIndex, firstIndex + length));
            Geometry geometryText = view.TextViewLines.GetMarkerGeometry(span);

            var textLineOwning = view.TextViewLines[indexTextLine];
            var spanOwning = new SnapshotSpan(view.TextSnapshot, textLineOwning.Start, 0);

            if (geometryText != null)
            {
                var newRect = new Rect()
                {
                    X = geometryText.Bounds.X,
                    Y = geometryText.Bounds.Y,
                    Width = geometryText.Bounds.Width,
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

                // Align the image with the top of the bounds of the text geometryText
                Canvas.SetLeft(image, geometryText.Bounds.Left);
                Canvas.SetTop(image, textLineOwning.Top);
                layer.AddAdornment(AdornmentPositioningBehavior.TextRelative, spanOwning, null, image, null);
            }

        }
    }
}
