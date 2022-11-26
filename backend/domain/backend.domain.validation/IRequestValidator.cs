using backend.domain.Request;
using Range = backend.domain.Limits.Range;

namespace backend.domain.validation
{
	public interface IRequestValidator
	{
		ValidationResult Validate(PriceRequest request, Limits.Range weightRange, Range volumeRange);
	}
}
