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

            // Find first non-tab character and count non-tab/non-space characters
            int firstNonTabIndex = -1;
            int nonTabNonSpaceCount = 0;

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != '\t')
                {
                    if (firstNonTabIndex == -1)
                    {
                        firstNonTabIndex = i;
                    }
                    if (text[i] != ' ')
                    {
                        nonTabNonSpaceCount++;
                    }
                }
            }

            //String only consists of tabs, is valid thus return false;
            if (firstNonTabIndex == -1 || text[firstNonTabIndex] != ' ')
            {
                return false;
            }
            //String only consists tabs and spaces, is valid thus return false
            if (nonTabNonSpaceCount == 0)
            {
                return false;
            }
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
            if (text.Length == tabString.Length)
            {
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] != tabString[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
    }
}
