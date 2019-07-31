namespace IndentRainbow.Logic.Classification
{
    public class IndentValidator : IIndentValidator
    {

        public string indentation = "    ";
        public string tab = "\t";
        public IndentValidator(int indentSize)
        {
            string accumulator = "";
            for (int i = 0; i < indentSize; i++)
            {
                accumulator += " ";
            }
            this.indentation = accumulator;
        }

        public int GetIndentBlockLength()
        {
            return this.indentation.Length;
        }

        public bool IsIncompleteIndent(string text)
        {
            string cleaned = text.Replace("\t", "");
            //String only consists of tabs, is valid thus return false;
            if (cleaned.Length == 0)
            {
                return false;
            }
            //String only consists tabs and spaces, is valid thus return false
            if (cleaned.Replace(" ", "").Length == 0)
            {
                return false;
            }
            // Checking if first character is 
            if (cleaned[0] == ' ')
            {
                return !this.IsValidIndent(text);
            }
            return false;
        }

        public bool IsValidIndent(string text)
        {
            if (text.Equals(this.indentation)
                || text.Equals(this.tab))
            {
                return true;
            }
            return false;
        }
    }
}
