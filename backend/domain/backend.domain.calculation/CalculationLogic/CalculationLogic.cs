using backend.utilities;

namespace backend.domain.calculation.CalculationLogic
{
	public class CalculationLogic : ICalculationLogic
	{
		private readonly IEnumerable<PriceBracket> _priceBrackets;

		public CalculationLogic(IEnumerable<PriceBracket> priceBrackets)
		{
			priceBrackets = priceBrackets.NotNull(nameof(priceBrackets));

			_priceBrackets = OrderByPriceAscending(priceBrackets);
		}

		public decimal Calculate(decimal value)
		{
			try
			{
				return _priceBrackets.First(bracket => bracket.Range.Includes(value)).Price;
			}
			catch (InvalidOperationException)
			{
				throw new Exception("Requested value does not match any price bracket");
			}
		}

		private static IEnumerable<PriceBracket> OrderByPriceAscending(IEnumerable<PriceBracket> priceBrackets)
		{
			return priceBrackets.OrderBy(pb => pb.Price);
		}
	}
}
