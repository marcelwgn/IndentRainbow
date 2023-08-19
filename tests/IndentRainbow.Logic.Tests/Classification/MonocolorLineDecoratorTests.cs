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
	public class MonoColorLineDecoratorTests
	{
		/// <summary>
		/// Four Space Indent constant;
		/// This name was chosen for easy creation of tests
		/// </summary>
		private const string FSI = "    ";
		private const string TABI = "\t";

		private MonocolorLineDecorator decorator;
		private readonly Mock<IBackgroundTextIndexDrawer> backgroundTextIndexDrawerMock = new Mock<IBackgroundTextIndexDrawer>();
		private IBackgroundTextIndexDrawer BackgroundTextIndexDrawer => backgroundTextIndexDrawerMock.Object;
		private readonly IndentValidator validator = new IndentValidator(4);
		private readonly RainbowBrushGetter rainbowgetter = new RainbowBrushGetter(new Color[] {
				Color.FromRgb(0,0,0)
			}, new SolidColorBrush(), ColorMode.Solid, false);

		[TestInitialize]
		public void Setup()
		{
			decorator = new MonocolorLineDecorator(BackgroundTextIndexDrawer, rainbowgetter, validator);
		}

		[DataTestMethod]
		[DataRow(FSI + FSI + TABI + FSI + "t", 13, 3)]
		[DataRow(TABI + FSI + "123456789", 5, 1)]
		[DataRow(TABI + "123456789", 1, 0)]
		[DataRow(TABI + TABI + TABI + "123456789", 3, 2)]
		[DataRow(TABI + "1", 1, 0)]
		[DataRow(TABI + TABI + TABI + "1", 3, 2)]
		[DataRow(TABI + TABI + TABI + TABI + TABI + TABI + TABI + "1", 7, 6)]
		[DataRow(FSI + "text" + FSI, 4, 0)]
		[DataRow("", null, -1)]
		[DataRow(FSI + FSI + "12345", 8, 1)]
		public void DecorateLineTests_IndexTesting_ExpectedBehavior(string text, int? possibleLength, int colorIndex)
		{
			var randomDrawIndexCutoff = new Random().Next(2 << 16);
			decorator.DecorateLine(text, randomDrawIndexCutoff);

			if (possibleLength != null)
			{
				backgroundTextIndexDrawerMock.Verify(
					p => p.DrawBackground(
						randomDrawIndexCutoff,
						It.IsIn(possibleLength.Value),
						rainbowgetter.GetColorByIndex(colorIndex, 0)),
					Times.Once()
				);
			}
			else
			{
				backgroundTextIndexDrawerMock.Verify(
					p => p.DrawBackground(
							It.IsAny<int>(),
							It.IsAny<int>(),
							It.IsAny<Brush>()
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
			var itCount = text.Length / FSI.Length;
			var sequence = new MockSequence();
			for (var i = 0; i < itCount; i++)
			{
				backgroundTextIndexDrawerMock.InSequence(sequence).Setup(
					p => p.DrawBackground(
						It.IsAny<int>(),
						It.IsAny<int>(),
						It.IsAny<Brush>()
					)
				);
			}

			decorator.DecorateLine(text, 0);

			for (var i = 0; i < itCount; i++)
			{
				backgroundTextIndexDrawerMock.InSequence(sequence).Setup(
					p => p.DrawBackground(
						It.IsAny<int>(),
						It.IsAny<int>(),
						rainbowgetter.GetColorByIndex(i, 0)
					)
				);
			}
		}


		[DataTestMethod]
		[DataRow(FSI + FSI + TABI + FSI + " t", 14)]
		[DataRow(TABI + FSI + " 123456789", 6)]
		[DataRow(FSI + " text" + FSI, 5)]
		[DataRow(FSI + FSI + " 12345", 9)]
		[DataRow("test", null)]
		public void DecorateLineTests_IndexTesting_ErrorBehaviors(string text, int? endPosition)
		{
			var randomDrawIndexCutoff = new Random().Next(2 << 16);
			decorator.DecorateLine(text, randomDrawIndexCutoff);

			if (endPosition != null)
			{
				backgroundTextIndexDrawerMock.Verify(
					p => p.DrawBackground(
						randomDrawIndexCutoff,
						endPosition.Value,
						It.IsAny<Brush>()),
					Times.Once()
				);
				backgroundTextIndexDrawerMock.Verify(
					p => p.DrawBackground(
							It.IsNotIn(new int[] { randomDrawIndexCutoff, endPosition.Value }),
							It.IsNotIn(4),
							It.IsAny<Brush>()
						),
					Times.Never()
				);
			}
		}

		[DataTestMethod]
		[DataRow(FSI + FSI + TABI + FSI + " t", 14)]
		[DataRow(TABI + FSI + " 123456789", 6)]
		[DataRow(FSI + " text" + FSI, 5)]
		[DataRow(FSI + FSI + " 12345", 9)]
		public void DecorateLineTests_NoErrorDetection_ErrorBehaviors(string text, int endOffset)
		{
			decorator.detectErrors = false;
			var randomDrawIndexCutoff = new Random().Next(2 << 16);
			decorator.DecorateLine(text, randomDrawIndexCutoff);

			backgroundTextIndexDrawerMock.Verify(
				p => p.DrawBackground(
					randomDrawIndexCutoff,
					endOffset,
					It.IsAny<Brush>()),
				Times.Once()
			);
		}
	}
}