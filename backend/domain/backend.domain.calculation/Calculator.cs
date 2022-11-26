using backend.domain.calculation.CalculationLogic;
using backend.domain.Request;
using backend.utilities;

namespace backend.domain.calculation
{
	public class Calculator : ICalculator
	{
		private readonly ICalculationLogic _weightCalculationLogic;
		private readonly ICalculationLogic _volumeCalculationLogic;

		public Calculator(ICalculationLogic weightCalculationLogic, ICalculationLogic volumeCalculationLogic)
		{
			_weightCalculationLogic = weightCalculationLogic.NotNull(nameof(weightCalculationLogic));
			_volumeCalculationLogic = volumeCalculationLogic.NotNull(nameof(volumeCalculationLogic));
		}

		public decimal Calculate(PriceRequest request)
		{
			var priceBasedOnWeight = _weightCalculationLogic.Calculate(request.Weight);
			var priceBasedOnVolume = _volumeCalculationLogic.Calculate(request.Dimensions.Volume);

			return Math.Max(priceBasedOnWeight, priceBasedOnVolume);
		}
	}
}
