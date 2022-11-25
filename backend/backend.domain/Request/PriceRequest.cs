namespace backend.domain.Request
{
	public class PriceRequest
	{
		public PriceRequest(Weight weight, Dimensions dimensions)
		{
			Weight = weight;
			Dimensions = dimensions;
		}

		public Weight Weight { get; }
		public Dimensions Dimensions { get; }
	}
}
