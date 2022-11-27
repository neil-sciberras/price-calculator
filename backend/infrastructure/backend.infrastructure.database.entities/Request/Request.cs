using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace backend.infrastructure.database.entities.Request
{
	[ExcludeFromCodeCoverage]
	public class Request
	{
		[Key]
		public int Id { get; set; }
		public decimal Weight { get; set; }
		public decimal Width { get; set; }
		public decimal Height { get; set; }
		public decimal Depth { get; set; }
		public DateTime Date { get; set; }
	}
}
