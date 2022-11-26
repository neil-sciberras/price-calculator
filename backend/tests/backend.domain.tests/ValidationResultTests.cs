using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace backend.domain.tests
{
	public class ValidationResultTests
	{
		private static IEnumerable<object[]> ValidationResultsScenarios()
		{
			yield return new object[]
			{
				new List<ValidationResult>()
				{
					new ValidationResult(true, null),
					new ValidationResult(false, "message 1")
				},
				false,
				"message 1"
			};

			yield return new object[]
			{
				new List<ValidationResult>()
				{
					new ValidationResult(false, "message 1"),
					new ValidationResult(false, "message 2")
				},
				false,
				"message 1, message 2"
			};

			yield return new object[]
			{
				new List<ValidationResult>()
				{
					new ValidationResult(false, "message 1"),
					new ValidationResult(true, "message 2")
				},
				false,
				"message 1"
			};

			yield return new object[]
			{
				new List<ValidationResult>()
				{
					new ValidationResult(false, "message 1"),
					new ValidationResult(false, "message 2"),
					new ValidationResult(false, "message 3")
				},
				false,
				"message 1, message 2, message 3"
			};
		}

		[Test, TestCaseSource(nameof(ValidationResultsScenarios))]
		public void GivenTwoValidationResults_ThenCombineWithReturnsCorrectly(
			IEnumerable<ValidationResult> validationResults, 
			bool expectedFinalSuccess, 
			string expectedFinalMessage)
		{
			// Act
			var finalValidationResult = validationResults.Aggregate(func: (result, validationResult) => result.CombineWith(validationResult));

			// Assert
			Assert.AreEqual(expectedFinalSuccess, finalValidationResult.Success);
			Assert.AreEqual(expectedFinalMessage, finalValidationResult.Message);
		}
	}
}
