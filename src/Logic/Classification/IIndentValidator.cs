namespace IndentRainbow.Logic.Classification
{
    public interface IIndentValidator
    {

        /// <summary>
        /// Checks if a given string is a valid indentation
        /// </summary>
        /// <param name="text">The indent to prove</param>
        /// <returns>Returns true if the string is a valid indent, false if not</returns>
        bool IsValidIndent(string text);

        /// <summary>
        /// Gets the length of the current indent block e.g. 4 for 4 space indentation
        /// </summary>
        /// <returns>The length of an indent block</returns>
        int GetIndentBlockLength();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">The text to be analyzed</param>
        /// <returns>Returns true, when the given string is an incomplete/incorrect indent, otherwise false</returns>
        bool IsIncompleteIndent(string text);
    }
}
