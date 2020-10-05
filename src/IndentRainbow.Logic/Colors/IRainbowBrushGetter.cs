using System.Windows.Media;

namespace IndentRainbow.Logic.Colors
{
    public interface IRainbowBrushGetter
    {

        /// <summary>
        /// Returns the color specified by the index. 
        /// Indeces larger than the list of colors, should be treated at wrapping around to the start
        /// </summary>
        /// <param name="rainbowIndex">The index of the color (starting at 0)</param>
        /// <returns>The Brush that sits at the index specified</returns>
        Brush GetColorByIndex(int rainbowIndex);

        /// <summary>
        /// This method is a getter for the color used for wrong/faulty indentation
        /// </summary>
        /// <returns>Returns the color used for error in indentation</returns>
        Brush GetErrorBrush();
    }
}
