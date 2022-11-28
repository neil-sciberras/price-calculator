using backend.infrastructure.database.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace backend.infrastructure.database
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
			=> services
				.AddSqlite<RequestDb>(connectionString)
				.AddScoped<IRequestRepository, RequestRepository>();
	}
}
