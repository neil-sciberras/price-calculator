using Microsoft.Extensions.DependencyInjection;

namespace backend.domain.validation
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddDomainValidationDependencies(this IServiceCollection services)
			=> services.AddSingleton<IRequestValidator, RequestValidator>();
	}
}
