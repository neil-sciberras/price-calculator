using backend.domain.Request;

namespace backend.domain.validation
{
	public interface IRequestValidator
	{
		ValidationResult Validate(PriceRequest request);
	}
}
