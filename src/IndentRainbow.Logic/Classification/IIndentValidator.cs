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
        /// Sets the indentation size of this IndentValidator
        /// </summary>
        /// <param name="indentSize">The new size of the indentation</param>
        void SetIndentation(int  indentSize);

        /// <summary>
        /// Returns how many indentation levels are present in the substring presented
        /// </summary>
        /// <param name="text">The text that will be parsed</param>
        /// <param name="start">The start position of the indentation</param>
        /// <param name="length">The length of the indentation block</param>
        /// <returns></returns>
		int GetIndentLevelCount(string text, int length);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="text">The text to be analyzed</param>
		/// <returns>Returns true, when the given string is an incomplete/incorrect indent, otherwise false</returns>
		bool IsIncompleteIndent(string text);
    }
}
