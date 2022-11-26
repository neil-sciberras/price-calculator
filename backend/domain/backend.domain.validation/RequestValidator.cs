﻿using backend.domain.Request;
using Range = backend.domain.Limits.Range;

namespace backend.domain.validation
{
	public class RequestValidator : IRequestValidator
	{
		public ValidationResult Validate(PriceRequest request, Range weightRange, Range volumeRange)
		{
			var weightResult = Validator.Validate(weightRange, request.Weight, "Weight");
			var volumeResult = Validator.Validate(volumeRange, request.Dimensions.Volume, "Volume");

			return weightResult.CombineWith(volumeResult);
		}
	}
}