using IndentRainbow.Logic.Text;

namespace IndentRainbow.Logic.Tests
{
    internal readonly struct StringTextSpan : ITextSpan
    {
        private readonly string text;

        public StringTextSpan(string text)
        {
            this.text = text;
        }

        public char this[int index] => text[index];

        public int Length => text.Length;

        public static implicit operator StringTextSpan(string text) => new StringTextSpan(text);
    }
}
