namespace backend.domain.Limits
{
	public class Limit
	{
		public Limit(int value, bool inclusive)
		{
			Value = value;
			Inclusive = inclusive;
		}

		public int Value { get; }
		public bool Inclusive { get; }

		public string GetMessage()
		{
			var equalsOrEmpty = Inclusive ? "=" : string.Empty;
			return $"{equalsOrEmpty} {Value}";
		}
	}
}
