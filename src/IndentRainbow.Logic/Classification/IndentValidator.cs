using IndentRainbow.Logic.Text;

namespace IndentRainbow.Logic.Classification
{
    public class IndentValidator : IIndentValidator
    {

        private string indentation = "    ";
        private const string tabString = "\t";
        public IndentValidator(int indentSize)
        {
            SetIndentation(indentSize);
        }

        public void SetIndentation(int indentSize)
        {
            indentation = new string(' ', indentSize);
        }

        public int GetIndentBlockLength()
        {
            return indentation.Length;
        }

        public bool IsIncompleteIndent(ITextSpan text)
        {
            if (text == null)
            {
                return false;
            }

            bool isTabOnly = true; // Determine if whitespaces are only tabs, if so, we don't want to mark them as incomplete indents
            int firstNonSpaceIndex = -1;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (c != ' ')
                {
                    firstNonSpaceIndex = i;
                    break;
                }

                isTabOnly &= (c == '\t');
            }

            if (isTabOnly || firstNonSpaceIndex == -1)
                return false;

            // Checking if the rest is a valid indent 
            return !IsValidIndent(text);
        }

        public int GetIndentLevelCount(ITextSpan text, int length)
        {
            var tabCount = 0;
            var spaceCount = 0;
            if (text == null)
            {
                return 0;
            }
            for (int i = 0; i < length; i++)
            {
                if (text[i] == '\t')
                {
                    tabCount++;
                }
                else if (text[i] == ' ')
                {
                    spaceCount++;
                }
            }
            return tabCount + (spaceCount / GetIndentBlockLength());
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
        public bool IsValidIndent(ITextSpan text)
        {
            if (text.Length == indentation.Length)
            {
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] != indentation[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            if (text.Length == 1 && text[0] == '\t')
            {
                return true;
            }
            return false;
        }
    }
}
