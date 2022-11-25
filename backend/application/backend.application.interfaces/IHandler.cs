using backend.api.contracts.Request;

namespace backend.application.interfaces
{
	public interface IHandler
	{
		decimal Handle(PriceRequest priceRequest);
	}
}
