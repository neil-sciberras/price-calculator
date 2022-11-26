using System.Diagnostics.CodeAnalysis;

namespace backend.api.contracts.Request
{
	/// <summary>
	/// Represents the customer's request for price calculation
	/// </summary>
	[ExcludeFromCodeCoverage]
	public class PriceRequest
	{
		public decimal Weight { get; set; }
		public Dimensions Dimensions { get; set; }
	}
}
