using IndentRainbow.Logic.Colors;
using NUnit.Framework;
using System.Windows.Media;

namespace IndentRainbow.LogicTests.Colors
{
    [TestFixture]
    public class ColorParserTests
    {
        private static readonly Brush[][] solutions = new Brush[][]
        {
            //First test case
            new Brush[]
            {
                new SolidColorBrush(Color.FromArgb(255, 255, 255, 255))
            },
            new Brush[]
            {
                new SolidColorBrush(Color.FromArgb(0x40, 255, 255, 0)),
                new SolidColorBrush(Color.FromArgb(0x40, 102, 255, 51)),
                new SolidColorBrush(Color.FromArgb(0x40, 0, 204, 255)),
                new SolidColorBrush(Color.FromArgb(0x40, 153, 51, 255)),
                new SolidColorBrush(Color.FromArgb(0x40, 255, 0, 255)),
                new SolidColorBrush(Color.FromArgb(0x40, 255, 0, 0)),
                new SolidColorBrush(Color.FromArgb(0x40, 255, 170, 0))
            }

        };


        [Test]
        [TestCase("#FFFFFFFF", 0)]
        [TestCase("#40FFFF00,#4066FF33,#4000CCFF,#409933FF,#40FF00FF,#40FF0000,#40FFAA00",1)]
        public void ConvertStringToBrushArray_ExpectedBehaviour(string input, int solutionIndex)
        {
            Brush[] result = ColorParser.ConvertStringToBrushArray(input);
            Brush[] solution = solutions[solutionIndex];

            Assert.AreEqual(solution.Length, result.Length);
            for (int i = 0; i < solution.Length; i++)
            {
                Assert.AreEqual(solution[i].ToString(), result[i].ToString());
            }
        }
    }
}
