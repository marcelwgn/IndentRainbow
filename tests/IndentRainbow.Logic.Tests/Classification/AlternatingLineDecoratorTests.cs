using System;
using System.Windows.Media;
using IndentRainbow.Logic.Classification;
using IndentRainbow.Logic.Colors;
using IndentRainbow.Logic.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using static IndentRainbow.Logic.Parser.ColorParser;

namespace IndentRainbow.Logic.Tests.Classification
{
    [TestClass]
    public class AlternatingLineDecoratorTests
    {
        /// <summary>
        /// Four Space Indent constant;
        /// This name was chosen for easy creation of tests
        /// </summary>
        private const string FSI = "    ";
        private const string TABI = "\t";

        private AlternatingLineDecorator decorator;
        private readonly Mock<IBackgroundTextIndexDrawer> backgroundTextIndexDrawerMock = new Mock<IBackgroundTextIndexDrawer>();
        private IBackgroundTextIndexDrawer backgroundTextIndexDrawer => backgroundTextIndexDrawerMock.Object;
        private readonly IndentValidator validator = new IndentValidator(4);
        private readonly RainbowBrushGetter rainbowgetter = new RainbowBrushGetter(new Color[] {
                Color.FromRgb(0, 0, 0)
            }, new SolidColorBrush(), ColorMode.Solid, false);


        [TestInitialize]
        public void Setup()
        {
            decorator = new AlternatingLineDecorator(backgroundTextIndexDrawer, rainbowgetter, validator);
        }

        [DataTestMethod]
        [DataRow(FSI + FSI + TABI + FSI + "t", new int[] { 0, 4, 8, 9, 13 })]
        [DataRow(TABI + FSI + "123456789", new int[] { 0, 1, 5 })]
        [DataRow(TABI + "123456789", new int[] { 0, 1 })]
        [DataRow(TABI + TABI + TABI + "123456789", new int[] { 0, 1, 2, 3 })]
        [DataRow(TABI + "1", new int[] { 0, 1 })]
        [DataRow(TABI + TABI + TABI + "1", new int[] { 0, 1, 2, 3 })]
        [DataRow(TABI + TABI + TABI + TABI + TABI + TABI + TABI + "1", new int[] { 0, 1, 2, 3, 4, 5, 6, 7 })]
        [DataRow(FSI + "text" + FSI, new int[] { 0 })]
        [DataRow("", new int[] { })]
        [DataRow(FSI + FSI + "12345", new int[] { 0, 4 })]
        public void DecorateLineTests_IndexTesting_ExpectedBehavior(string text, int[] spans)
        {
            var randomDrawIndexCutoff = new Random().Next(2 << 16);
            decorator.DecorateLine(text, randomDrawIndexCutoff, 0);

            if (spans.Length > 0)
            {
                for (var i = 0; i < spans.Length - 1; i++)
                {
                    backgroundTextIndexDrawerMock.Verify(
                        p => p.DrawBackground(
                            spans[i] + randomDrawIndexCutoff,
                            It.IsIn(new int[] { FSI.Length, TABI.Length }),
                            It.IsAny<Brush>(),
                        It.IsAny<int>()),
                        Times.Once()
                    );
                }
            }
            else
            {
                backgroundTextIndexDrawerMock.Verify(
                    p => p.DrawBackground(
                            It.IsAny<int>(),
                            It.IsAny<int>(),
                            It.IsAny<Brush>(),
                        It.IsAny<int>()
                        ),
                    Times.Never()
                );
            }
        }

        [DataTestMethod]
        [DataRow(FSI + FSI + FSI + FSI + FSI + FSI + FSI)]
        [DataRow(FSI + "dsadsadsa")]
        [DataRow(FSI + "  dsadsa")]
        [DataRow(TABI + FSI + "  dsadsa")]
        [DataRow(FSI + TABI + "  dsadsa")]
        public void DecorateLineTests_ColorTesting_ExpectedBehavior(string text)
        {
            var sequence = new MockSequence();
            for (var i = 0; i < text.Length / FSI.Length; i++)
            {
                backgroundTextIndexDrawerMock.InSequence(sequence).Setup(
                    p => p.DrawBackground(
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        It.IsAny<Brush>(),
                        It.IsAny<int>()
                    )
                );
            }

            decorator.DecorateLine(text, 0, 0);

            for (var i = 0; i < text.Length / FSI.Length; i++)
            {
                backgroundTextIndexDrawerMock.InSequence(sequence).Setup(
                    p => p.DrawBackground(
                        It.IsAny<int>(),
                        It.IsAny<int>(),
                        rainbowgetter.GetColorByIndex(i, 0),
                        It.IsAny<int>()
                    )
                );
            }
        }


        [DataTestMethod]
        [DataRow(FSI + FSI + TABI + FSI + " t", 14)]
        [DataRow(TABI + FSI + " 123456789", 6)]
        [DataRow(FSI + " text" + FSI, 5)]
        [DataRow(FSI + FSI + " 12345", 9)]
        public void DecorateLineTests_IndexTesting_ErrorBehaviors(string text, int endOffset)
        {
            var randomDrawIndexCutoff = new Random().Next(2 << 16);
            decorator.DecorateLine(text, randomDrawIndexCutoff, 0);

            backgroundTextIndexDrawerMock.Verify(
                p => p.DrawBackground(
                    randomDrawIndexCutoff,
                    endOffset,
                    It.IsAny<Brush>(),
                        It.IsAny<int>()),
                Times.Once()
            );
            backgroundTextIndexDrawerMock.Verify(
                p => p.DrawBackground(
                        It.IsNotIn(new int[] { randomDrawIndexCutoff, endOffset }),
                        It.IsNotIn(4),
                        It.IsAny<Brush>(),
                        It.IsAny<int>()
                    ),
                Times.Never()
            );
        }

        [DataTestMethod]
        [DataRow(FSI + FSI + TABI + FSI + " t", 4)]
        [DataRow(TABI + FSI + " 123456789", 1)]
        [DataRow(FSI + " text" + FSI, 4)]
        [DataRow(FSI + FSI + " 12345", 4)]
        public void DecorateLineTests_NoErrorDetection_ErrorBehaviors(string text, int endOffset)
        {
            decorator.detectErrors = false;
            var randomDrawIndexCutoff = new Random().Next(2 << 16);
            decorator.DecorateLine(text, randomDrawIndexCutoff, 0);

            backgroundTextIndexDrawerMock.Verify(
                p => p.DrawBackground(
                    randomDrawIndexCutoff,
                    endOffset,
                    It.IsAny<Brush>(),
                        It.IsAny<int>()),
                Times.Once()
            );
        }
    }
}