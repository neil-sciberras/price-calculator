using backend.application.interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace backend.application
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
			=> services.AddScoped<IHandler, Handler>();
	}
}
