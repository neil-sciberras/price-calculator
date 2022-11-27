using System.Diagnostics.CodeAnalysis;

namespace backend.api.contracts.PersistedData
{
	[ExcludeFromCodeCoverage]
	public class Request
	{
		public int Id { get; set; }
		public decimal Weight { get; set; }
		public decimal Width { get; set; }
		public decimal Height { get; set; }
		public decimal Depth { get; set; }
		public DateTime Date { get; set; }
	}
}
