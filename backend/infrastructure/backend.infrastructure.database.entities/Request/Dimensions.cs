using System.Diagnostics.CodeAnalysis;

namespace backend.infrastructure.database.entities.Request
{
	[ExcludeFromCodeCoverage] 
	public class Dimensions
	{
		public decimal Width { get; set; }
		public decimal Height { get; set; }
		public decimal Depth { get; set; }
	}
}
