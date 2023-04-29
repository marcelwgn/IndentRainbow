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

		private static readonly Color[] emptyColors = Array.Empty<Color>();

		private RainbowBrushGetter solidBrushGetterNormal;
		private RainbowBrushGetter solidBrushGetterFading;
		private RainbowBrushGetter gradientBrushGetterNormal;
		private RainbowBrushGetter gradientBrushGetterFading;

		[TestInitialize]
		public void Setup()
		{
			solidBrushGetterNormal = new RainbowBrushGetter(colors, new SolidColorBrush(System.Windows.Media.Colors.Red), ColorMode.Solid, false);
			solidBrushGetterFading = new RainbowBrushGetter(colors, new SolidColorBrush(System.Windows.Media.Colors.Red), ColorMode.Solid, true);
			gradientBrushGetterNormal = new RainbowBrushGetter(colors, new SolidColorBrush(System.Windows.Media.Colors.Red), ColorMode.Gradient, false);
			gradientBrushGetterFading = new RainbowBrushGetter(colors, new SolidColorBrush(System.Windows.Media.Colors.Red), ColorMode.Gradient, true);
		}

		[DataTestMethod]
		[DataRow(false, ColorMode.Solid, false)]
		[DataRow(true, ColorMode.Solid, false)]
		[DataRow(false, ColorMode.Solid, true)]
		[DataRow(true, ColorMode.Solid, true)]
		[DataRow(false, ColorMode.Gradient, false)]
		[DataRow(true, ColorMode.Gradient, false)]
		[DataRow(false, ColorMode.Gradient, true)]
		[DataRow(true, ColorMode.Gradient, true)]
		public void HandlesFaultyColorsInput(bool nullColors, ColorMode colorMode, bool fadeColors)
		{
			var colors = nullColors ? null : Array.Empty<Color>();
			var getter = new RainbowBrushGetter(colors, new SolidColorBrush(System.Windows.Media.Colors.Red), colorMode, fadeColors);
			Assert.IsNotNull(getter);
			Assert.IsNull(getter.GetColorByIndex(0, 1));
			Assert.IsNull(getter.GetColorByIndex(0, 5));
			Assert.IsNull(getter.GetColorByIndex(2, 5));
		}

		[DataTestMethod]
		[DataRow(0, 0)]
		[DataRow(1, 1)]
		[DataRow(2, 2)]
		[DataRow(3, 0)]
		[DataRow(4, 1)]
		public void GetColorByIndex_SolidNormal_ColorsAreCorrect(int index, int internalTestIndex)
		{
			var result = solidBrushGetterNormal.GetColorByIndex(index, 5);

			Assert.AreEqual(solidColorBrushes[internalTestIndex].ToString(), result.ToString());
		}

		[DataTestMethod]
		[DataRow(0, 0)]
		[DataRow(1, 1)]
		[DataRow(2, 2)]
		[DataRow(3, 0)]
		[DataRow(4, 1)]
		public void GetColorByIndex_GradientNormal_ColorsAreCorrect(int index, int equivalentIndex)
		{
			var result = gradientBrushGetterNormal.GetColorByIndex(index, 5);
			var equivalentBrush = gradientBrushGetterNormal.GetColorByIndex(equivalentIndex, 5);

			Assert.IsInstanceOfType<LinearGradientBrush>(result);
			Assert.AreEqual(equivalentBrush, result);
			if (result is LinearGradientBrush linearBrush)
			{
				Assert.AreEqual(colors[equivalentIndex], linearBrush.GradientStops[0].Color);
			}
		}

		[DataTestMethod]
		public void GetColorByIndex_SolidFading_SingleColorIsCorrect()
		{
			var result = solidBrushGetterFading.GetColorByIndex(0, 1);
			Assert.IsInstanceOfType<LinearGradientBrush>(result);
			if (result is LinearGradientBrush linearBrush)
			{
				Assert.AreEqual(System.Windows.Media.Colors.Transparent, linearBrush.GradientStops[0].Color);
				Assert.AreEqual(System.Windows.Media.Colors.Red, linearBrush.GradientStops[1].Color);
				Assert.AreEqual(System.Windows.Media.Colors.Transparent, linearBrush.GradientStops[2].Color);
			}
		}

		[DataTestMethod]
		public void GetColorByIndex_GradientFading_SingleColorIsCorrect()
		{
			var result = gradientBrushGetterFading.GetColorByIndex(0, 1);
			Assert.IsInstanceOfType<LinearGradientBrush>(result);
			if (result is LinearGradientBrush linearBrush)
			{
				Assert.AreEqual(System.Windows.Media.Colors.Transparent, linearBrush.GradientStops[0].Color);
				Assert.AreEqual(System.Windows.Media.Colors.Red, linearBrush.GradientStops[1].Color);
				Assert.AreEqual(System.Windows.Media.Colors.Transparent, linearBrush.GradientStops[2].Color);
			}
		}

		[DataTestMethod]
		public void GetColorByIndex_SolidFading_ColorFadingInIsCorrect()
		{
			var result = solidBrushGetterFading.GetColorByIndex(0, 5);
			Assert.IsInstanceOfType<LinearGradientBrush>(result);
			if (result is LinearGradientBrush linearBrush)
			{
				Assert.AreEqual(System.Windows.Media.Colors.Transparent, linearBrush.GradientStops[0].Color);
				Assert.AreEqual(System.Windows.Media.Colors.Red, linearBrush.GradientStops[1].Color);
				Assert.AreEqual(System.Windows.Media.Colors.Red, linearBrush.GradientStops[2].Color);
			}
		}

		[DataTestMethod]
		public void GetColorByIndex_GradientFading_ColorFadingInIsCorrect()
		{
			var result = gradientBrushGetterFading.GetColorByIndex(0, 5);
			Assert.IsInstanceOfType<LinearGradientBrush>(result);
			if (result is LinearGradientBrush linearBrush)
			{
				Assert.AreEqual(System.Windows.Media.Colors.Transparent, linearBrush.GradientStops[0].Color);
				Assert.AreEqual(System.Windows.Media.Colors.Red, linearBrush.GradientStops[1].Color);
				Assert.AreEqual(System.Windows.Media.Colors.Green, linearBrush.GradientStops[2].Color);
			}
		}


		[DataTestMethod]
		[DataRow(1)]
		[DataRow(2)]
		[DataRow(3)]
		[DataRow(4)]
		public void GetColorByIndex_SolidFading_ColorsFadingOutAreCorrect(int index)
		{
			var correctColor = colors[index % colors.Length];
			var result = solidBrushGetterFading.GetColorByIndex(index, index + 1);
			Assert.IsInstanceOfType<LinearGradientBrush>(result);
			if (result is LinearGradientBrush linearBrush)
			{
				Assert.AreEqual(correctColor, linearBrush.GradientStops[0].Color);
				Assert.AreEqual(correctColor, linearBrush.GradientStops[1].Color);
				Assert.AreEqual(System.Windows.Media.Colors.Transparent, linearBrush.GradientStops[2].Color);
			}
		}

		[DataTestMethod]
		[DataRow(1)]
		[DataRow(2)]
		[DataRow(3)]
		[DataRow(4)]
		public void GetColorByIndex_GradientFading_ColorsFadingOutAreCorrect(int index)
		{
			var correctColor = colors[index % colors.Length];
			var result = gradientBrushGetterFading.GetColorByIndex(index, index + 1);
			Assert.IsInstanceOfType<LinearGradientBrush>(result);
			if (result is LinearGradientBrush linearBrush)
			{
				Assert.AreEqual(correctColor, linearBrush.GradientStops[0].Color);
				Assert.AreEqual(correctColor, linearBrush.GradientStops[1].Color);
				Assert.AreEqual(System.Windows.Media.Colors.Transparent, linearBrush.GradientStops[2].Color);
			}
		}

		[TestMethod]
		public void GetColorByIndex_SolidFading_ColorWithNegativeColumnsCountIsSolid()
		{
			var result = solidBrushGetterFading.GetColorByIndex(0, -1);
			Assert.IsInstanceOfType<SolidColorBrush>(result);
			if (result is SolidColorBrush brush)
			{
				Assert.AreEqual(colors[0], brush.Color);
			}
		}

		[TestMethod]
		public void GetColorByIndex_GradientFading_ColorWithNegativeColumnsCountIsSolid()
		{
			var result = gradientBrushGetterFading.GetColorByIndex(0, -1);
			Assert.IsInstanceOfType<LinearGradientBrush>(result);
			if (result is LinearGradientBrush brush)
			{
				Assert.AreEqual(colors[0], brush.GradientStops[0].Color);
				Assert.AreEqual(colors[1], brush.GradientStops[1].Color);
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
					solidBrushGetterNormal.GetColorByIndex(index, 5);
				});
		}

		[DataTestMethod]
		public void GetColorByIndex_EmptyCollectionHandling()
		{
			solidBrushGetterNormal = new RainbowBrushGetter(Array.Empty<Color>(), null, ColorMode.Solid, false);
			Assert.IsNull(solidBrushGetterNormal.GetColorByIndex(1, 3));
		}

		[DataTestMethod]
		public void GetErrorBrush_Fading_ExpectedBehavior()
		{
			Assert.IsNotNull(solidBrushGetterNormal.ErrorBrush);
		}
	}
}
