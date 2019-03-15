using System.Windows.Media;

namespace IndentRainbow.Logic.Drawing
{
    public interface IBackgroundTextIndexDrawer
    {

        /// <summary>
        /// Draws a background box starting at the given index, with the length specified in the given color.
        /// The index and length are being specified as the character index or amount of characters to cover.
        /// </summary>
        /// <param name="firstIndex">The index of the character to start the box in (inclusively)</param>
        /// <param name="length">The length of the background box to draw,
        /// specified in characters</param>
        /// <param name="drawBrush">The Brush to use for drawing</param>
        void DrawBackground(int firstIndex, int length, Brush drawBrush);
    }
}
