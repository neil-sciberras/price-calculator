using System.Diagnostics.CodeAnalysis;
using backend.application.interfaces;

namespace backend.application
{
	[ExcludeFromCodeCoverage]
	public class DateService : IDateService
	{
		public DateTime GetDate()
		{
			return DateTime.UtcNow;
		}
	}
}
