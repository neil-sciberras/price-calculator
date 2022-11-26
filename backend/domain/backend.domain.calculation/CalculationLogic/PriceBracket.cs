using Range = backend.domain.Limits.Range;

namespace backend.domain.calculation.CalculationLogic
{
	public class PriceBracket
	{
		public Range Range;
		public decimal Price;

		public PriceBracket(Range range, decimal price)
		{
			Range = range;
			Price = price;
		}
	}
}
