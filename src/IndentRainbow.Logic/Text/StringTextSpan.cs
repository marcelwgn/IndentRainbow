namespace IndentRainbow.Logic.Text
{
    public readonly struct StringTextSpan : ITextSpan
    {
        private readonly string text;

        public StringTextSpan(string text)
        {
            this.text = text;
        }

        public char this[int index] => text[index];

        public int Length => text?.Length ?? 0;

        public static implicit operator StringTextSpan(string text) => new StringTextSpan(text);
    }
}
