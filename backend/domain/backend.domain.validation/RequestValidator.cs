using backend.domain.Request;
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
			var weightResult = Validator.Validate(_weightRange, request.Weight, "Weight");
			var volumeResult = Validator.Validate(_volumeRange, request.Dimensions.Volume, "Volume");

			return weightResult.CombineWith(volumeResult);
		}
	}
}
