using System.Diagnostics.CodeAnalysis;
using backend.application.interfaces;
using backend.partners;
using Microsoft.Extensions.DependencyInjection;

namespace backend.application
{
	[ExcludeFromCodeCoverage]
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
			=> services
				.AddPartnerDependencies()
				.AddSingleton<IDateService, DateService>()
				.AddScoped<IPersistedDataService, PersistedDataService>()
				.AddScoped<IPriceService, PriceService>();
	}
}
