using System;
using System.Windows.Media;
using IndentRainbow.Logic.Colors;
using IndentRainbow.Logic.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IndentRainbow.Logic.Parser.ColorParser;

namespace IndentRainbow.Logic.Tests.Colors
{
	[TestClass]
	public class RainbowBrushGetterTests
	{
		private static readonly Color[] colors = new Color[]
		{
			System.Windows.Media.Colors.Red,
			System.Windows.Media.Colors.Green,
			System.Windows.Media.Colors.Blue
		};

		private static readonly SolidColorBrush[] solidColorBrushes = new SolidColorBrush[]
		{
			new SolidColorBrush(colors[0]),
			new SolidColorBrush(colors[1]),
			new SolidColorBrush(colors[2])
		};

		private RainbowBrushGetter solidBrushGetter;
		private RainbowBrushGetter gradientBrushGetter;

		[TestInitialize]
		public void Setup()
		{
			solidBrushGetter = new RainbowBrushGetter(colors, new SolidColorBrush(System.Windows.Media.Colors.Red), ColorMode.Solid);
			gradientBrushGetter = new RainbowBrushGetter(colors, new SolidColorBrush(System.Windows.Media.Colors.Red), ColorMode.Gradient);
		}

		[DataTestMethod]
		[DataRow(0, 0)]
		[DataRow(1, 1)]
		[DataRow(2, 2)]
		[DataRow(3, 0)]
		[DataRow(4, 1)]
		public void GetColorByIndex_ExpectedBehavior(int index, int internalTestIndex)
		{
			var result = solidBrushGetter.GetColorByIndex(index);

			Assert.AreEqual(solidColorBrushes[internalTestIndex].ToString(), result.ToString());
		}


		[DataTestMethod]
		[DataRow(0, 0)]
		[DataRow(1, 1)]
		[DataRow(2, 2)]
		[DataRow(3, 0)]
		[DataRow(4, 1)]
		public void GetColorByIndex_ExpectedBehavior_GradientMode(int index, int equivalentIndex)
		{
			var result = gradientBrushGetter.GetColorByIndex(index);
			var equivalentBrush = gradientBrushGetter.GetColorByIndex(equivalentIndex);

			Assert.IsInstanceOfType<LinearGradientBrush>(result);
			Assert.AreEqual(equivalentBrush, result);
			if (result is LinearGradientBrush linearBrush)
			{
				Assert.AreEqual(colors[equivalentIndex], linearBrush.GradientStops[0].Color);
			}
		}

		[DataTestMethod]
		[DataRow(-1)]
		[DataRow(-200)]
		public void GetColorByIndex_ErrorHandling(int index)
		{
			Assert.ThrowsException<ArgumentOutOfRangeException>(
				delegate
				{
					solidBrushGetter.GetColorByIndex(index);
				});
		}

		[DataTestMethod]
		public void GetColorByIndex_EmptyCollectionHandling()
		{
			solidBrushGetter = new RainbowBrushGetter(Array.Empty<Color>(), null, ColorMode.Solid);
			Assert.IsNull(solidBrushGetter.GetColorByIndex(1));
		}

		[DataTestMethod]
		public void GetErrorBrush_ExpectedBehavior()
		{
			Assert.IsNotNull(solidBrushGetter.ErrorBrush);
		}
	}
}
