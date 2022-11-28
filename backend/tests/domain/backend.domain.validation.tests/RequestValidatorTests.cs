using System.Collections.Generic;
using backend.domain.Limits;
using backend.domain.Request;
using backend.domain.validation.Exceptions;
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
			var validator = GetValidator();

			// Act
			var validationResult = validator.Validate(priceRequest);

			// Assert
			Assert.AreEqual(expectedSuccess, validationResult.Success);
			Assert.AreEqual(expectedMessage, validationResult.Message);
		}

		private static IEnumerable<object> InvalidRequests()
		{
			yield return new PriceRequest(weight: -1, dimensions: new Dimensions(1, 1, 1));
			yield return new PriceRequest(weight: 0, dimensions: new Dimensions(1, 1, 1));
			yield return new PriceRequest(weight: 1, dimensions: new Dimensions(-1, 1, 1));
			yield return new PriceRequest(weight: 1, dimensions: new Dimensions(1, 0, 1));
		}

		[Test, TestCaseSource(nameof(InvalidRequests))]
		public void GivenAnInvalidRequest_ThenValidateThrows(PriceRequest request)
		{
			// Arrange
			var validator = GetValidator();

			// Act
			var exception = Assert.Throws<InvalidRequestException>(() => validator.Validate(request));

			// Assert
			Assert.NotNull(exception);
			Assert.AreEqual("Invalid request. All values must be greater than zero.", exception!.Message);
		}

		private RequestValidator GetValidator()
		{
			var weightRange = new Range(
				lowerLimit: new Limit(10, true),
				upperLimit: new Limit(15, false));

			var volumeRange = new Range(
				lowerLimit: new Limit(1000, false),
				upperLimit: new Limit(4000, true));

			return new RequestValidator(weightRange: weightRange, volumeRange: volumeRange);
		}
	}
}
