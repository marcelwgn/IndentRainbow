namespace IndentRainbow.Logic.Classification
{
    public class IndentValidator : IIndentValidator
    {

        private string indentation = "    ";

        public int GetIndentBlockLength()
        {
            return this.indentation.Length;
        }

        public bool IsIncompleteIndent(string text)
        {
            if (text[0] == ' ')
            {
                return !this.IsValidIndent(text);
            }
            return false;
        }

        public bool IsValidIndent(string text)
        {
            if (text.Equals(this.indentation))
            {
                return true;
            }
            return false;
        }

        public void SetIndentLevel(string indentation)
        {
            this.indentation = indentation;
        }
    }
}
