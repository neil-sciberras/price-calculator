using backend.domain.calculation.CalculationLogic;
using Microsoft.Extensions.DependencyInjection;

namespace backend.domain.calculation
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddDomainValidationDependencies(this IServiceCollection services)
		{
			return services
				.AddSingleton<ICalculationLogic, CalculationLogic.CalculationLogic>()
				.AddSingleton<ICalculationLogic, CalculationLogicWithExtraRate>()
				.AddSingleton<ICalculationLogic, CalculationLogic.CalculationLogicWithExtraRate>();
		}
	}
}
