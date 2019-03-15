using AutoMoq;
using IndentRainbow.Logic.Classification;
using IndentRainbow.Logic.Drawing;
using IndentRainbow.Logic.Colors;
using Moq;
using NUnit.Framework;
using System;
using System.Windows.Media;

namespace IndentRainbow.LogicTests.Classification
{
    [TestFixture]
    public class LineDecoratorTests
    {
        /// <summary>
        /// Four Space Indent constant;
        /// This name was chosen for easy creation of tests
        /// </summary>
        private const string FSI = "    ";

        private AutoMoqer mocker;
        private LineDecorator decorator;
        private readonly IndentValidator validator = new IndentValidator();
        private readonly RainbowBrushGetter rainbowgetter = new RainbowBrushGetter();


        [SetUp]
        public void Setup()
        {
            this.mocker = new AutoMoqer();

            this.validator.SetIndentLevel(FSI);
            this.mocker.SetInstance<IIndentValidator>(this.validator);

            this.mocker.SetInstance<IRainbowBrushGetter>(this.rainbowgetter);

            this.decorator = this.mocker.Resolve<LineDecorator>();
        }

        [Test]
        [TestCase(FSI + FSI + FSI, 0, 12, new int[] { 0, 4, 8 })]
        [TestCase(FSI + " 123456789", 0, 14, new int[] { 0 })]
        [TestCase(FSI + "text" + FSI, 0, 12, new int[] { 0 })]
        [TestCase("", 0, 0, new int[] { })]
        [TestCase("1234567890" + FSI + FSI + "12345", 10, 23, new int[] { 10, 14 })]
        public void DecorateLineTests_IndexTesting_ExpectedBehaviour(string text, int start, int end, int[] spans)
        {
            this.decorator.DecorateLine(text, start, end);

            for (int i = 0; i < spans.Length - 1; i++)
            {
                this.mocker.Verify<IBackgroundTextIndexDrawer>(
                    p => p.DrawBackground(
                        spans[i], FSI.Length,
                        It.IsAny<Brush>()),
                    Times.Once()
                );
            }
            this.mocker.Verify<IBackgroundTextIndexDrawer>(
                p => p.DrawBackground(
                        It.IsNotIn(spans),
                        It.IsNotIn(4),
                        It.IsAny<Brush>()
                    ),
                Times.Never()
            );
        }

        [Test]
        [TestCase(FSI, -1, 2, typeof(ArgumentOutOfRangeException))]
        [TestCase(FSI, 2, 1, typeof(ArgumentException))]
        [TestCase(FSI, 20, 22, typeof(ArgumentOutOfRangeException))]
        [TestCase(FSI, 2, 20, typeof(ArgumentOutOfRangeException))]
        [TestCase(FSI, 0, -2, typeof(ArgumentOutOfRangeException))]
        public void DecorateLineTests_IndexTesting_ErrorHandling(string text, int start, int end, Type exceptionType)
        {
            Assert.Throws(exceptionType,
                delegate
                {
                    this.decorator.DecorateLine(text, start, end);
                });
        }

        [Test]
        [TestCase(FSI + FSI + FSI + FSI + FSI + FSI + FSI)]
        [TestCase(FSI + "dsadsadsa")]
        [TestCase(FSI + "  dsadsa")]
        public void DecorateLineTests_ColorTesting_ExpectedBehaviour(string text)
        {
            int itCount = text.Length / FSI.Length;
            var sequence = new MockSequence();
            var colorMock = this.mocker.GetMock<IBackgroundTextIndexDrawer>();
            for (int i = 0; i < itCount; i++)
            {
                colorMock.InSequence(sequence).Setup(
                    p => p.DrawBackground(
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<Brush>()
                    )
                );
            }

            this.decorator.DecorateLine(text, 0, text.Length);

            for (int i = 0; i < itCount; i++)
            {
                colorMock.InSequence(sequence).Setup(
                    p => p.DrawBackground(
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        this.rainbowgetter.GetColorByIndex(i)
                    )
                );
            }
        }

    }
}
