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
		private IBackgroundTextIndexDrawer backgroundTextIndexDrawer => backgroundTextIndexDrawerMock.Object;
		private readonly IndentValidator validator = new IndentValidator(4);
		private readonly RainbowBrushGetter rainbowgetter = new RainbowBrushGetter(new Color[] {
				Color.FromRgb(0,0,0)
			}, new SolidColorBrush(), ColorMode.Solid);

		[TestInitialize]
		public void Setup()
		{
			decorator = new MonocolorLineDecorator(backgroundTextIndexDrawer, rainbowgetter, validator);
		}

		[DataTestMethod]
		[DataRow(FSI + FSI + TABI + FSI + "t", 0, 13, new int[] { 0, 13 }, 3)]
		[DataRow(TABI + FSI + "123456789", 0, 14, new int[] { 0, 5 }, 1)]
		[DataRow(TABI + "123456789", 0, 10, new int[] { 0, 1 }, 0)]
		[DataRow(TABI + TABI + TABI + "123456789", 0, 10, new int[] { 0, 3 }, 2)]
		[DataRow(TABI + "1", 0, 2, new int[] { 0, 1 }, 0)]
		[DataRow(TABI + TABI + TABI + "1", 0, 4, new int[] { 0, 3 }, 2)]
		[DataRow(TABI + TABI + TABI + TABI + TABI + TABI + TABI + "1", 0, 8, new int[] { 0, 7 }, 6)]
		[DataRow(FSI + "text" + FSI, 0, 12, new int[] { 0 }, 0)]
		[DataRow("", 0, 0, new int[] { }, -1)]
		[DataRow("1234567890" + FSI + FSI + "12345", 10, 23, new int[] { 10, 18 }, 1)]
		public void DecorateLineTests_IndexTesting_ExpectedBehavior(string text, int start, int end, int[] spans, int colorIndex)
		{
			decorator.DecorateLine(text, start, end);

			if (spans.Length > 1)
			{
				var correctLength = spans[1] - spans[0];

				backgroundTextIndexDrawerMock.Verify(
					p => p.DrawBackground(
						spans[0], It.IsIn(correctLength),
						rainbowgetter.GetColorByIndex(colorIndex, 0)),
					Times.Once()
				);
			}

			backgroundTextIndexDrawerMock.Verify(
				p => p.DrawBackground(
						It.IsNotIn(spans),
						It.IsNotIn(4),
						It.IsAny<Brush>()
					),
				Times.Never()
			);
		}

		[DataTestMethod]
		[DataRow(FSI, -1, 2)]
		[DataRow(FSI, 2, 1)]
		[DataRow(FSI, 20, 22)]
		[DataRow(FSI, 2, 20)]
		[DataRow(FSI, 0, -2)]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Not needed here")]
		public void DecorateLineTests_IndexTesting_ErrorHandling(string text, int start, int end)
		{
			try
			{
				decorator.DecorateLine(text, start, end);
				Assert.Fail();
			}
			catch (Exception)
			{
				Assert.IsTrue(true);
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

			decorator.DecorateLine(text, 0, text.Length);

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
		[DataRow(FSI + FSI + TABI + FSI + " t", 0, 15, new int[] { 0, 14 })]
		[DataRow(TABI + FSI + " 123456789", 0, 14, new int[] { 0, 6 })]
		[DataRow(FSI + " text" + FSI, 0, 12, new int[] { 0, 5 })]
		[DataRow("1234567890" + FSI + FSI + " 12345", 10, 23, new int[] { 10, 9 })]
		[DataRow("test", 0, 4, null)]
		public void DecorateLineTests_IndexTesting_ErrorBehaviors(string text, int start, int end, int[] spans)
		{
			decorator.DecorateLine(text, start, end);

			if (spans != null)
			{
				backgroundTextIndexDrawerMock.Verify(
					p => p.DrawBackground(
						spans[0], spans[1],
						It.IsAny<Brush>()),
					Times.Once()
				);
				backgroundTextIndexDrawerMock.Verify(
					p => p.DrawBackground(
							It.IsNotIn(spans),
							It.IsNotIn(4),
							It.IsAny<Brush>()
						),
					Times.Never()
				);
			}
		}

		[DataTestMethod]
		[DataRow(FSI + FSI + TABI + FSI + " t", 0, 15, new int[] { 0, 14 })]
		[DataRow(TABI + FSI + " 123456789", 0, 14, new int[] { 0, 6 })]
		[DataRow(FSI + " text" + FSI, 0, 12, new int[] { 0, 5 })]
		[DataRow("1234567890" + FSI + FSI + " 12345", 10, 23, new int[] { 10, 9 })]
		public void DecorateLineTests_NoErrorDetection_ErrorBehaviors(string text, int start, int end, int[] spans)
		{
			decorator.detectErrors = false;
			decorator.DecorateLine(text, start, end);

			backgroundTextIndexDrawerMock.Verify(
				p => p.DrawBackground(
					spans[0], spans[1],
					It.IsAny<Brush>()),
				Times.Once()
			);
		}
	}
}