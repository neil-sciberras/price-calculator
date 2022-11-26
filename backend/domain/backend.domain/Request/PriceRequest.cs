namespace backend.domain.Request
{
	public class PriceRequest
	{
		public PriceRequest(decimal weight, Dimensions dimensions)
		{
			Weight = weight;
			Dimensions = dimensions;
		}

		public decimal Weight { get; }
		public Dimensions Dimensions { get; }
	}
}
