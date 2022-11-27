using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using backend.mappers;
using backend.utilities;
using Microsoft.Extensions.DependencyInjection;

namespace backend.partners
{
	[ExcludeFromCodeCoverage]
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddPartnerDependencies(this IServiceCollection services)
		{
			return services
				.AddMapper()
				.Add(PartnerFactory.Cargo4You)
				.Add(PartnerFactory.ShipFaster)
				.Add(PartnerFactory.MaltaShip);
		}

		private static IServiceCollection Add(this IServiceCollection services, string partnerName)
		{
			return services.AddSingleton(provider =>
			{
				var mapper = provider.GetService<IMapper>().NotNull("IMapper");
				return PartnerFactory.Create(mapper, partnerName);
			});
		}
	}
}
