using System;
using IndentRainbow.Logic.Text;
using Microsoft.VisualStudio.Text;

namespace IndentRainbow.Extension
{
    internal readonly struct SnapshotTextSpan : ITextSpan
    {
        private readonly ITextSnapshot snapshot;
        private readonly int start;

        public SnapshotTextSpan(ITextSnapshot snapshot, int start, int length)
        {
            this.snapshot = snapshot;
            this.start = start;
            Length = length;
        }

        public char this[int index]
        {
            get
            {
                if (index < 0 || index >= Length)
                    throw new ArgumentOutOfRangeException(nameof(index));
                return snapshot[start + index];
            }
        }

        public int Length { get; }
    }
}
