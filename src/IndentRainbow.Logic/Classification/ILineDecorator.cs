namespace IndentRainbow.Logic.Classification
{
    public interface ILineDecorator
    {

        void DecorateLine(string text, int startIndex, int endIndex);

    }
}