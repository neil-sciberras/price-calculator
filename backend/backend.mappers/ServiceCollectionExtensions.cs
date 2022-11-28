using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using backend.mappers.PersistedData;
using Microsoft.Extensions.DependencyInjection;

namespace backend.mappers
{
	[ExcludeFromCodeCoverage]
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddMapper(this IServiceCollection services)
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile<PriceRequestMappingProfile>();
				cfg.AddProfile<ValidationResultMappingProfile>();
				cfg.AddProfile<RequestMappingProfile>();
			});

			var mapper = config.CreateMapper();

			return services.AddSingleton(mapper);
		}
	}
}
