namespace IndentRainbow.Logic.Classification
{
    public class IndentValidator : IIndentValidator
    {

        private readonly string Indentation = "    ";
        public const string TabString = "\t";
        public IndentValidator(int indentSize)
        {
            var accumulator = "";
            for (var i = 0; i < indentSize; i++)
            {
                accumulator += " ";
            }
            Indentation = accumulator;
        }

        public int GetIndentBlockLength()
        {
            return Indentation.Length;
        }

        public bool IsIncompleteIndent(string text)
        {
            var cleaned = text?.Replace("\t", "");
            //String only consists of tabs, is valid thus return false;
            if (cleaned.Length == 0 || cleaned[0] != ' ')
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
                return !IsValidIndent(text);
            }
            return false;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "<Pending>")]
        public bool IsValidIndent(string text)
        {
            if (text.Equals(Indentation, System.StringComparison.InvariantCultureIgnoreCase)
                || text.Equals(TabString, System.StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }
    }
}
