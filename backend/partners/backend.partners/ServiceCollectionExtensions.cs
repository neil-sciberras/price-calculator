using AutoMapper;
using backend.domain.calculation;
using backend.domain.calculation.CalculationLogic;
using backend.domain.Limits;
using backend.domain.validation;
using backend.partners.interfaces;
using backend.utilities;
using Microsoft.Extensions.DependencyInjection;
using Range = backend.domain.Limits.Range;

namespace backend.partners
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddPartnerDependencies(this IServiceCollection services, IServiceProvider serviceProvider)
		{
			var mapper = serviceProvider.GetService<IMapper>().NotNull("IMapper");
			
			return services
				.AddCargo4YouPartner(mapper)
				.AddShipFasterPartner(mapper)
				.AddMaltaShipPartner(mapper);
		}

		private static IServiceCollection AddMaltaShipPartner(this IServiceCollection services, IMapper mapper)
		{
			var requestValidator = new RequestValidator(
				weightRange: new Range(lowerLimit: new Limit(10, inclusive: true)),
				volumeRange: new Range(upperLimit: new Limit(500, inclusive: true)));

			var weightCalculationLogic = new CalculationLogicWithExtraRate(
				priceBrackets: new List<PriceBracket>
				{
					new(new Range(lowerLimit: new Limit(10, inclusive: false), upperLimit: new Limit(20, inclusive: true)), price: 16.99m),
					new(new Range(lowerLimit: new Limit(20, inclusive: false), upperLimit: new Limit(30, inclusive: true)), price: 33.99m),
					new(new Range(lowerLimit: new Limit(30, inclusive: false)), price: 43.99m)
				},
				ratePerExtraKg: 0.41m);

			var volumeCalculationLogic = new CalculationLogic(new List<PriceBracket>
			{
				new(new Range(upperLimit: new Limit(1000, inclusive: true)), price: 9.5m),
				new(new Range(lowerLimit: new Limit(1000, inclusive: false), upperLimit: new Limit(2000, inclusive: true)), price: 19.5m),
				new(new Range(lowerLimit: new Limit(2000, inclusive: false), upperLimit: new Limit(5000, inclusive: true)), price: 48.5m),
				new(new Range(lowerLimit: new Limit(5000, inclusive: false)), price: 147.5m)
			});

			var calculator = new Calculator(weightCalculationLogic: weightCalculationLogic, volumeCalculationLogic: volumeCalculationLogic);

			return services.AddPartner(mapper, requestValidator, calculator);
		}

		private static IServiceCollection AddShipFasterPartner(this IServiceCollection services, IMapper mapper)
		{
			var requestValidator = new RequestValidator(
				weightRange: new Range(lowerLimit: new Limit(10, inclusive: false), upperLimit: new Limit(30, inclusive: true)),
				volumeRange: new Range(upperLimit: new Limit(1700, inclusive: true)));

			var weightCalculationLogic = new CalculationLogicWithExtraRate(
				priceBrackets: new List<PriceBracket>
				{
					new(new Range(lowerLimit: new Limit(10, inclusive: false), upperLimit: new Limit(15, inclusive: true)), price: 16.5m),
					new(new Range(lowerLimit: new Limit(15, inclusive: false), upperLimit: new Limit(25, inclusive: true)), price: 36.5m),
					new(new Range(lowerLimit: new Limit(25, inclusive: false)), price: 40)
				}, 
				ratePerExtraKg: 0.417m);

			var volumeCalculationLogic = new CalculationLogic(new List<PriceBracket>
			{
				new(new Range(upperLimit: new Limit(1000, inclusive: true)), price: 11.99m),
				new(new Range(lowerLimit: new Limit(1000, inclusive: false), upperLimit: new Limit(1700, inclusive: true)), price: 21.99m)
			});

			var calculator = new Calculator(weightCalculationLogic: weightCalculationLogic, volumeCalculationLogic: volumeCalculationLogic);

			return services.AddPartner(mapper, requestValidator, calculator);
		}

		private static IServiceCollection AddCargo4YouPartner(this IServiceCollection services, IMapper mapper)
		{
			var requestValidator = new RequestValidator(
				weightRange: new Range(upperLimit: new Limit(20, inclusive: true)),
				volumeRange: new Range(upperLimit: new Limit(2000, inclusive: true)));

			var weightCalculationLogic = new CalculationLogic(new List<PriceBracket>()
			{
				new(new Range(upperLimit: new Limit(2, inclusive: true)), price: 15),
				new(new Range(lowerLimit: new Limit(2, inclusive: false), upperLimit: new Limit(15, inclusive: true)), price: 18),
				new(new Range(lowerLimit: new Limit(15, inclusive: false), upperLimit: new Limit(20, inclusive: true)), price: 35)
			});

			var volumeCalculationLogic = new CalculationLogic(new List<PriceBracket>()
			{
				new(new Range(upperLimit: new Limit(1000, inclusive: true)), price: 10),
				new(new Range(lowerLimit: new Limit(1000, inclusive: false), upperLimit: new Limit(2000, inclusive: true)), price: 20)
			});

			var calculator = new Calculator(weightCalculationLogic: weightCalculationLogic, volumeCalculationLogic: volumeCalculationLogic);

			return services.AddPartner(mapper, requestValidator, calculator);
		}

		private static IServiceCollection AddPartner(this IServiceCollection services, IMapper mapper, IRequestValidator requestValidator, ICalculator calculator )
			=> services.AddSingleton<IPartner>(new Partner(
				mapper: mapper,
				requestValidator: requestValidator,
				calculator: calculator));
	}
}
