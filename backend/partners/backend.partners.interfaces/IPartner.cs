using backend.api.contracts;
using backend.api.contracts.Request;

namespace backend.partners.interfaces
{
	public interface IPartner
	{
		ValidationResult Validate(PriceRequest priceRequest);
		decimal Calculate(PriceRequest priceRequest);
	}
}
