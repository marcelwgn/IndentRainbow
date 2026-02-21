using System;

namespace IndentRainbow.Logic.Text
{
    internal readonly struct ReadTextSubSpan : ITextSpan
    {
        private readonly ITextSpan source;
        private readonly int offset;

        public ReadTextSubSpan(ITextSpan source, int offset, int length)
        {
            this.source = source;
            this.offset = offset;
            Length = length;
        }

        public char this[int index]
        {
            get
            {
                if (index < 0 || index >= Length)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return source[offset + index];
            }
        }

        public int Length { get; }
    }
}
