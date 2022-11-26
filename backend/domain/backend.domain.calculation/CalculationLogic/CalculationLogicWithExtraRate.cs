namespace backend.domain.calculation.CalculationLogic
{
	public class CalculationLogicWithExtraRate : CalculationLogic
	{
		private readonly decimal _ratePerExtraKg;
		private PriceBracket LastBracket => PriceBrackets.Last();

		public CalculationLogicWithExtraRate(IEnumerable<PriceBracket> priceBrackets, decimal ratePerExtraKg) : base(priceBrackets)
		{
			if (LastBracket.Range.LowerLimit == null || LastBracket.Range.UpperLimit != null)
			{
				throw new Exception("The last bracket should have the lower limit set (and only that)");
			}

			_ratePerExtraKg = ratePerExtraKg;
		}

		public override decimal Calculate(decimal value)
		{
			var basePrice = base.Calculate(value);

			if (basePrice == PriceBrackets.Last().Price)
			{
				return basePrice + CalculateAdditionalCost(value);
			}

			return basePrice;
		}

		private decimal CalculateAdditionalCost(decimal value)
		{
			var extraKgs = (int)Math.Ceiling(value - LastBracket.Range.LowerLimit.Value);

			return extraKgs * _ratePerExtraKg;
		}
	}
}
