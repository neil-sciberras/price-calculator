namespace backend.domain.Limits
{
	public class Range
	{
		public readonly Limit? LowerLimit;
		public readonly Limit? UpperLimit;

		public Range(Limit? lowerLimit = null, Limit? upperLimit = null)
		{
			if (lowerLimit  == null && upperLimit == null)
				throw new ArgumentException("At least one limit should have a value");

			LowerLimit = lowerLimit;
			UpperLimit = upperLimit;
		}
		
		public bool Includes(decimal value)
		{
			var greaterThanLower = true;
			var smallerThanUpper = true;

			if (LowerLimit != null)
			{
				greaterThanLower = LowerLimit.Inclusive
					? value >= LowerLimit.Value
					: value > LowerLimit.Value;
			}

			if (UpperLimit != null)
			{
				smallerThanUpper = UpperLimit.Inclusive 
					? value <= UpperLimit.Value
					: value < UpperLimit.Value;
			}

			return smallerThanUpper && greaterThanLower;
		}

		public string GetMessage()
		{
			var greaterThanMessageOrEmpty = LowerLimit != null ? $">{LowerLimit.GetMessage()}" : string.Empty;
			var smallerThanMessageOrEmpty = UpperLimit != null ? $"<{UpperLimit.GetMessage()}" : string.Empty;
			var andOrEmpty = LowerLimit != null && UpperLimit != null ? " and " : string.Empty;


			return $"should be {greaterThanMessageOrEmpty}{andOrEmpty}{smallerThanMessageOrEmpty}";
		}
	}
}
