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
    public class AlternatingLineDecoratorTests
    {
        /// <summary>
        /// Four Space Indent constant;
        /// This name was chosen for easy creation of tests
        /// </summary>
        private const string FSI = "    ";
        private const string TABI = "\t";

        private AutoMoqer mocker;
        private AlternatingLineDecorator decorator;
        private readonly IndentValidator validator = new IndentValidator(4);
        private readonly RainbowBrushGetter rainbowgetter = new RainbowBrushGetter();


        [SetUp]
        public void Setup()
        {
            this.mocker = new AutoMoqer();

            this.mocker.SetInstance<IIndentValidator>(this.validator);

            this.mocker.SetInstance<IRainbowBrushGetter>(this.rainbowgetter);

            this.decorator = this.mocker.Resolve<AlternatingLineDecorator>();
        }

        [Test]
        [TestCase(FSI + FSI + TABI + FSI + "t", 0, 13, new int[] { 0, 4, 8, 9,13 })]
        [TestCase(TABI + FSI + "123456789", 0, 14, new int[] { 0, 1,5 })]
        [TestCase(TABI + "123456789", 0, 10, new int[] { 0, 1 })]
        [TestCase(TABI + TABI + TABI + "123456789", 0, 10, new int[] { 0, 1, 2, 3 })]
        [TestCase(TABI +  "1", 0, 2, new int[] { 0, 1 })]
        [TestCase(TABI + TABI + TABI + "1", 0, 4, new int[] { 0, 1, 2, 3 })]
        [TestCase(TABI + TABI + TABI + TABI + TABI + TABI + TABI + "1", 0, 8, new int[] { 0, 1, 2, 3, 4, 5, 6, 7 })]
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
                        spans[i], It.IsIn(new int[] { FSI.Length, TABI.Length }),
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
        [TestCase(TABI + FSI + "  dsadsa")]
        [TestCase(FSI + TABI + "  dsadsa")]
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


        [Test]
        [TestCase(FSI + FSI + TABI + FSI + " t", 0, 15, new int[] { 0, 14 })]
        [TestCase(TABI + FSI + " 123456789", 0, 14, new int[] { 0, 6 })]
        [TestCase(FSI + " text" + FSI, 0, 12, new int[] { 0, 5 })]
        [TestCase("1234567890" + FSI + FSI + " 12345", 10, 23, new int[] { 10, 9 })]
        public void DecorateLineTests_IndexTesting_ErrorBehaviours(string text, int start, int end, int[] spans)
        {
            this.decorator.DecorateLine(text, start, end);

            this.mocker.Verify<IBackgroundTextIndexDrawer>(
                p => p.DrawBackground(
                    spans[0], spans[1],
                    It.IsAny<Brush>()),
                Times.Once()
            );
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
        [TestCase(FSI + FSI + TABI + FSI + " t", 0, 15, new int[] { 0, 4 })]
        [TestCase(TABI + FSI + " 123456789", 0, 14, new int[] { 0, 1 })]
        [TestCase(FSI + " text" + FSI, 0, 12, new int[] { 0, 4 })]
        [TestCase("1234567890" + FSI + FSI + " 12345", 10, 23, new int[] { 10, 4 })]
        public void DecorateLineTests_NoErrorDetection_ErrorBehaviours(string text, int start, int end, int[] spans)
        {
            this.decorator.detectErrors = false;
            this.decorator.DecorateLine(text, start, end);

            this.mocker.Verify<IBackgroundTextIndexDrawer>(
                p => p.DrawBackground(
                    spans[0], spans[1],
                    It.IsAny<Brush>()),
                Times.Once()
            );
        }
    }
}