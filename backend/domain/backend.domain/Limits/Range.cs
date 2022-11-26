namespace backend.domain.Limits
{
	public class Range
	{
		private readonly Limit? _lowerLimit;
		private readonly Limit? _upperLimit;

		public Range(Limit? lowerLimit = null, Limit? upperLimit = null)
		{
			_lowerLimit = lowerLimit;
			_upperLimit = upperLimit;
		}
		
		public bool Includes(decimal value)
		{
			var greaterThanLower = true;
			var smallerThanUpper = true;

			if (_lowerLimit != null)
			{
				greaterThanLower = _lowerLimit.Inclusive
					? value >= _lowerLimit.Value
					: value > _lowerLimit.Value;
			}

			if (_upperLimit != null)
			{
				smallerThanUpper = _upperLimit.Inclusive 
					? value <= _upperLimit.Value
					: value < _upperLimit.Value;
			}

			return smallerThanUpper && greaterThanLower;
		}

		public string GetMessage()
		{
			var greaterThanMessageOrEmpty = _lowerLimit != null ? $">{_lowerLimit.GetMessage()}" : string.Empty;
			var smallerThanMessageOrEmpty = _upperLimit != null ? $" and <{_upperLimit.GetMessage()}" : string.Empty;

			return $"should be {greaterThanMessageOrEmpty}{smallerThanMessageOrEmpty}";
		}
	}
}
