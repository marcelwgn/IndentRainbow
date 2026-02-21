namespace IndentRainbow.Logic.Text
{
    public interface ITextSpan
    {
        /// <summary>
        /// Gets the character at the specified index
        /// </summary>
        char this[int index] { get; }

        /// <summary>
        /// Gets the length of the text span
        /// </summary>
        int Length { get; }
    }
}
