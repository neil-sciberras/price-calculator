using backend.domain.Request;
using backend.domain.validation.Exceptions;
using Range = backend.domain.Limits.Range;

namespace backend.domain.validation
{
	public class RequestValidator : IRequestValidator
	{
		private readonly Range _weightRange;
		private readonly Range _volumeRange;

		public RequestValidator(Range weightRange, Range volumeRange)
		{
			_weightRange = weightRange;
			_volumeRange = volumeRange;
		}

		public ValidationResult Validate(PriceRequest request)
		{
			var basicValidationResult = BasicValidation(request);

			if (!basicValidationResult.Success)
			{
				throw new InvalidRequestException(basicValidationResult.Message!);
			}

			var weightResult = Validator.Validate(_weightRange, request.Weight, "Weight");
			var volumeResult = Validator.Validate(_volumeRange, request.Dimensions.Volume, "Volume");

			return weightResult.CombineWith(volumeResult);
		}

		private static ValidationResult BasicValidation(PriceRequest request)
		{
			if (request.Weight <= 0 || request.Dimensions.Volume <= 0)
			{
				return new ValidationResult(success: false, message: "Invalid request. All values must be greater than zero.");
			}

			return new ValidationResult(success: true, message: null);
		}
	}
}
