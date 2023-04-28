using System;
using System.Windows.Media;
using IndentRainbow.Logic.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static IndentRainbow.Logic.Parser.ColorParser;

namespace IndentRainbow.Logic.Tests.Colors
{
	[TestClass]
	public class ColorParserTests
	{
		private static readonly Color[][] solutionsSolidColorMode = new Color[][]
		{
            //First test case
            new Color[]
			{
				Color.FromArgb(255, 255, 255, 255)
			},
			new Color[]
			{
				Color.FromArgb(0x40, 255, 255, 0),
				Color.FromArgb(0x40, 102, 255, 51),
				Color.FromArgb(0x40, 0, 204, 255),
				Color.FromArgb(0x40, 153, 51, 255),
				Color.FromArgb(0x40, 255, 0, 255),
				Color.FromArgb(0x40, 255, 0, 0),
				Color.FromArgb(0x40, 255, 170, 0)
			},
			new Color[]
			{
				Color.FromArgb(0x40, 153, 51, 255),
				Color.FromArgb(0x40, 255, 0, 255),
				Color.FromArgb(0x40, 255, 0, 0),
				Color.FromArgb(0x40, 255, 170, 0)
			},
			new Color[]
			{
				Color.FromArgb(0x20,255,255,0),
				Color.FromArgb(0x20,0,255,255),
				Color.FromArgb(0x20,255,0,255),
			},
			Array.Empty<Color>(),
		};

		[DataTestMethod]
		[DataRow("#FFFFFFFF", 1.0, ColorMode.Solid, 0)]
		[DataRow("#40FFFF00,#4066FF33,#4000CCFF,#409933FF,#40FF00FF,#40FF0000,#40FFAA00", 1.0, ColorMode.Solid, 1)]
		[DataRow("#40FFFF004066FF33,#F,#409933FF,#40FF00FF,#40FF0000,#40FFAA00", 1.0, ColorMode.Solid, 2)]
		[DataRow("#40FFFF00,#4000FFFF,#40FF00FF", 0.5, ColorMode.Solid, 3)]
		[DataRow("#60FFFF00,#6000FFFF,#60FF00FF", 1.0 / 3.0, ColorMode.Solid, 3)]
		[DataRow(null, 0, ColorMode.Solid, 4)]
		[DataRow("", 0, ColorMode.Solid, 4)]
		public void ConvertStringBrushArray_ExpectedBehavior_SolidColorMode(string input, double opacityMultiplier, ColorMode colorMode, int solutionIndex)
		{
			var result = ColorParser.ConvertStringToColorArray(input, opacityMultiplier);
			var solution = solutionsSolidColorMode[solutionIndex];

			Assert.AreEqual(solution.Length, result.Length);
			for (var i = 0; i < solution.Length; i++)
			{
				Assert.AreEqual(solution[i].ToString(), result[i].ToString());
			}
		}

		[DataTestMethod]
		[DataRow("#FFFFFFFF", 1.0, 0)]
		[DataRow("#40FFFF00", 1.0, 1)]
		[DataRow("#409933FF", 1.0, 2)]
		[DataRow("#40FFFF00", 0.5, 3)]
		[DataRow("#60FFFF00", 1.0 / 3.0, 3)]
		[DataRow(null, 0, 0)]
		[DataRow("", 0, 0)]
		[DataRow("", -1, 0)]
		public void ConvertStringToBrush_ExpectedBehavior(string input, double opacityMultiplier, int solutionIndex)
		{
			var result = ColorParser.ConvertStringToBrush(input, opacityMultiplier);
			var solution = solutionsSolidColorMode[solutionIndex][0];
			if (result is null)
			{
				return;
			}
			Assert.AreEqual(solution.ToString(), result.ToString());
		}
	}
}
