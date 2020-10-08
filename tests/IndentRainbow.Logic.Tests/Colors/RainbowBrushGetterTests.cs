using System;
using System.Windows.Media;
using IndentRainbow.Logic.Colors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IndentRainbow.Logic.Tests.Colors
{
    [TestClass]
    public class RainbowBrushGetterTests
    {
        private static readonly Brush[] brushes = new Brush[]
        {
            new SolidColorBrush(System.Windows.Media.Colors.Red),
            new SolidColorBrush(System.Windows.Media.Colors.Green),
            new SolidColorBrush(System.Windows.Media.Colors.Blue)
        };

        private RainbowBrushGetter brushGetter;


        [TestInitialize]
        public void Setup()
        {
            brushGetter = new RainbowBrushGetter(brushes, null);
        }

        [DataTestMethod]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(2, 2)]
        [DataRow(3, 0)]
        [DataRow(4, 1)]
        public void GetColorByIndex_ExpectedBehavior(int index, int internalTestIndex)
        {
            var result = brushGetter.GetColorByIndex(index);

            Assert.AreEqual(brushes[internalTestIndex], result);
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(-200)]
        public void GetColorByIndex_ErrorHandling(int index)
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                delegate
                {
                    brushGetter.GetColorByIndex(index);
                });
        }

        [DataTestMethod]
        public void GetColorByIndex_EmptyCollectionHandling()
        {
            brushGetter = new RainbowBrushGetter(Array.Empty<Brush>(), null);
            Assert.IsNull(brushGetter.GetColorByIndex(1));
        }

        [DataTestMethod]
        public void GetErrorBrush_ExpectedBehavior()
        {
            Assert.IsNotNull(new RainbowBrushGetter().GetErrorBrush());
        }
    }
}
