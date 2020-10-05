using System;
using System.Windows.Media;
using IndentRainbow.Logic.Colors;
using NUnit.Framework;

namespace IndentRainbow.Logic.Tests.Colors
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
            brushGetter = new RainbowBrushGetter(brushes, null);
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 0)]
        [TestCase(4, 1)]
        public void GetColorByIndex_ExpectedBehavior(int index, int internalTestIndex)
        {
            var result = brushGetter.GetColorByIndex(index);

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
                    brushGetter.GetColorByIndex(index);
                });
        }

        [Test]
        public void GetColorByIndex_EmptyCollectionHandling()
        {
            brushGetter = new RainbowBrushGetter(Array.Empty<Brush>(), null);
            Assert.IsNull(brushGetter.GetColorByIndex(1));
        }

        [Test]
        public void GetErrorBrush_ExpectedBehavior()
        {
            Assert.NotNull(new RainbowBrushGetter().GetErrorBrush());
        }
    }
}
