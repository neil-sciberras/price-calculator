namespace backend.api.contracts.Request
{
	/// <summary>
	/// The dimensions (in centimeters) of the package to be shipped
	/// </summary>
	public class Dimensions
	{
		public decimal Width { get; set; }
		public decimal Height { get; set; }
		public decimal Depth { get; set; }
	}
}
