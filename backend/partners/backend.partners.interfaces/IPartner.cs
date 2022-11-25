using backend.api.contracts;
using backend.api.contracts.Request;

namespace backend.partners.interfaces
{
	public interface IPartner
	{
		decimal Calculate(PriceRequest priceRequest);
		ValidationResult Validate(PriceRequest priceRequest);
	}
}
