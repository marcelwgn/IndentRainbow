using IndentRainbow.Logic.Colors;
using NUnit.Framework;
using System;
using System.Windows.Media;

namespace IndentRainbow.LogicTests.Colors
{
    [TestFixture]
    public class RainbowBrushGetterTests
    {
        private static readonly Brush[] brushes = new Brush[]
        {
            new SolidColorBrush(System.Windows.Media.Colors.Red),
            new SolidColorBrush(System.Windows.Media.Colors.Green),
            new SolidColorBrush(System.Windows.Media.Colors.Blue)
        };

        private RainbowBrushGetter brushGetter;


        [SetUp]
        public void Setup()
        {
            this.brushGetter = new RainbowBrushGetter
            {
                brushes = brushes
            };
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 0)]
        [TestCase(4, 1)]
        public void GetColorByIndex_ExpectedBehavior(int index, int internalTestIndex)
        {
            var result = this.brushGetter.GetColorByIndex(index);

            Assert.AreEqual(brushes[internalTestIndex], result);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-200)]
        public void GetColorByIndex_ErrorHandling(int index)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                delegate
                {
                    this.brushGetter.GetColorByIndex(index);
                });
        }

        [Test]
        public void GetColorByIndex_EmptyCollectionHandling()
        {
            this.brushGetter.brushes = new Brush[0];
            Assert.IsNull(this.brushGetter.GetColorByIndex(1));
        }

        [Test]
        public void GetErrorBrush_ExpectedBehaviour()
        {
            Assert.NotNull(new RainbowBrushGetter().GetErrorBrush());
        }
    }
}
