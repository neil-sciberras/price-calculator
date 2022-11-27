using System.Diagnostics.CodeAnalysis;

namespace backend.infrastructure.database.entities.Request
{
	[ExcludeFromCodeCoverage]
	public class PriceRequest
	{
		public decimal Weight { get; set; }
		public Dimensions Dimensions { get; set; }
	}
}
