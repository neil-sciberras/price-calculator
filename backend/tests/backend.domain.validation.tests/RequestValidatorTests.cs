using System.Collections.Generic;
using backend.domain.Limits;
using backend.domain.Request;
using Range = backend.domain.Limits.Range;
using NUnit.Framework;

namespace backend.domain.validation.tests
{
	public class RequestValidatorTests
	{
		private static IEnumerable<object[]> ValidateScenarios()
		{
			yield return new object[]
			{
				new PriceRequest(
					weight: 10,
					dimensions: new Dimensions(
						width: 10m,
						height: 15m,
						depth: 20m)),
				true, null
			};

			yield return new object[]
			{
				new PriceRequest(
					weight: 15,
					dimensions: new Dimensions(
						width: 10m,
						height: 15m,
						depth: 20m)),
				false, "Weight should be >= 10 and < 15"
			};

			yield return new object[]
			{
				new PriceRequest(
					weight: 15,
					dimensions: new Dimensions(
						width: 20m,
						height: 15m,
						depth: 20m)),
				false, "Weight should be >= 10 and < 15, Volume should be > 1000 and <= 4000"
			};
		}

		[Test, TestCaseSource(nameof(ValidateScenarios))]
		public void ValidateTest(
			PriceRequest priceRequest, 
			bool expectedSuccess,
			string expectedMessage)
		{
			// Arrange
			var weightRange = new Range(
				lowerLimit: new Limit(10, true),
				upperLimit: new Limit(15, false));

			var volumeRange = new Range(
				lowerLimit: new Limit(1000, false),
				upperLimit: new Limit(4000, true));

			// Act
			var validationResult = RequestValidator.Validate(priceRequest, weightRange, volumeRange);

			// Assert
			Assert.AreEqual(expectedSuccess, validationResult.Success);
			Assert.AreEqual(expectedMessage, validationResult.Message);
		}
	}
}
