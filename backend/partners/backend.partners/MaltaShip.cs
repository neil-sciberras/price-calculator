using AutoMapper;
using backend.domain.calculation;
using backend.domain.Limits;
using backend.domain.validation;
using Range = backend.domain.Limits.Range;

namespace backend.partners
{
	public class MaltaShip : PartnerBase
	{
		public MaltaShip(IMapper mapper, IRequestValidator requestValidator, ICalculator calculator)
			: base(
				mapper: mapper,
				requestValidator: requestValidator,
				calculator: calculator,
			weightRange: new Range(
				lowerLimit: new Limit(value: 10, inclusive: true),
				upperLimit: null),
			volumeRange: new Range(
				lowerLimit: new Limit(value: 500, inclusive: true),
				upperLimit: null))
		{
		}
	}
}