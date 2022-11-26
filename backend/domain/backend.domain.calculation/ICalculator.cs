using backend.domain.Request;

namespace backend.domain.calculation
{
	public interface ICalculator
	{
		decimal Calculate(PriceRequest request);
	}
}
