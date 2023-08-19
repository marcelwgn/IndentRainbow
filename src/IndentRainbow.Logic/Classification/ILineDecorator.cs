namespace IndentRainbow.Logic.Classification
{
    public interface ILineDecorator
    {
		/// <summary>
		/// Process the given string and draws the relevant highlighting at the right position, 
		/// if the <see cref="IIndentValidator"/> validates the positions
		/// </summary>
		/// <param name="text">The string of the text</param>
		/// <param name="drawingStartIndex">The starting position of the line relative to the total document (used for rendering)</param>
		void DecorateLine(string text, int drawStartIndex);
    }
}