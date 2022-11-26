using backend.utilities;

namespace backend.domain.calculation.CalculationLogic
{
	public class CalculationLogic : ICalculationLogic
	{
		protected readonly IEnumerable<PriceBracket> PriceBrackets;

		public CalculationLogic(IEnumerable<PriceBracket> priceBrackets)
		{
			priceBrackets = priceBrackets.NotNull(nameof(priceBrackets));

			PriceBrackets = OrderByPriceAscending(priceBrackets);
		}

		public virtual decimal Calculate(decimal value)
		{
			try
			{
				return PriceBrackets.First(bracket => bracket.Range.Includes(value)).Price;
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
