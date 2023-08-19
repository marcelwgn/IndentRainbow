using IndentRainbow.Logic.Classification;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IndentRainbow.Logic.Tests.Classification
{
	[TestClass]
	public class IndentValidatorTests
	{

		private IndentValidator validator;
		private const string FSI = "    ";
		private const string TABI = "\t";

		[TestInitialize]
		public void Setup()
		{
			validator = new IndentValidator(4);
		}

		[DataTestMethod]
		[DataRow("a", 1)]
		[DataRow("bb", 2)]
		[DataRow("ccc", 3)]
		[DataRow("", 0)]
		[DataRow(TABI, 1)]
		public void GetIndentBlockLengthTests_ExpectedBehaviors(string text, int length)
		{
			validator = new IndentValidator(text.Length);

			var result = validator.GetIndentBlockLength();

			Assert.AreEqual(length, result);
		}

		[DataTestMethod]
		[DataRow("text", 0, 0)]
		[DataRow(FSI + "text", 4, 1)]
		[DataRow(FSI + FSI + FSI + "text", 4, 1)]
		[DataRow(FSI + FSI + FSI + "text", 12, 3)]
		[DataRow(FSI + TABI + FSI + "text", 9, 3)]
		[DataRow(TABI + TABI + TABI + "text", 3, 3)]
		[DataRow(TABI + FSI + "text", 9, 2)]
		[DataRow(TABI + TABI + "text", 3, 2)]
		public void GetIndentLevelCount(string text, int length, int expectedValue)
		{
			Assert.AreEqual(expectedValue, validator.GetIndentLevelCount(text, length));
		}

		[DataTestMethod]
		[DataRow(FSI, false)]
		[DataRow(" d", true)]
		[DataRow("d", false)]
		[DataRow("   d", true)]
		[DataRow("   ", false)]
		[DataRow(TABI + TABI + "t", false)]
		[DataRow(TABI + TABI + " t", true)]
		[DataRow("te  ", false)]
		public void IsIncompleteIndentTests_ExpectedBehaviors(string text, bool isIncompleteIndent)
		{
			validator = new IndentValidator(FSI.Length);

			var result = validator.IsIncompleteIndent(text);

			Assert.AreEqual(isIncompleteIndent, result);
		}

		[DataTestMethod]
		[DataRow(FSI, true)]
		[DataRow(FSI + " ", false)]
		[DataRow(FSI + "d", false)]
		[DataRow("   d", false)]
		public void IsValidIndentTests_ExpectedBehaviors(string text, bool isValidIndent)
		{
			validator = new IndentValidator(FSI.Length);

			var result = validator.IsValidIndent(text);

			Assert.AreEqual(isValidIndent, result);
		}
	}
}
