using AutoMoq;
using IndentRainbow.Logic.Drawing;
using Microsoft.VisualStudio.Text.Editor;
using NUnit.Framework;
using System.Windows.Media;

namespace IndentRainbowLogicTests.Drawing
{
    [TestFixture]
    public class BackgroundTextIndexDrawerTests
    {

        private readonly AutoMoqer mocker = new AutoMoqer();
        private BackgroundTextIndexDrawer drawer;

        [SetUp]
        public void Setup()
        {

            var mockLines = this.mocker.GetMock<IWpfTextViewLineCollection>();
            //var geometryMock = new Mock<Geometry>(MockBehavior.Loose);
            mockLines.SetReturnsDefault(Geometry.Empty);
            this.drawer = this.mocker.Create<BackgroundTextIndexDrawer>();
        }


        [Test]
        [TestCase(0, 0, null)]
        public void DrawBackground_StateUnderTest_ExpectedBehavior(int firstIndex, int length, Brush drawBrush)
        {
            this.drawer.DrawBackground(
                firstIndex,
                length,
                drawBrush);
        }
    }
}
