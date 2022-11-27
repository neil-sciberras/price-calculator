using backend.api.contracts.Request;

namespace backend.application.interfaces
{
	public interface IPriceService
	{
		decimal GetPrice(PriceRequest priceRequest);
	}
}
