using AutoMapper;

using backend.domain.Limits;
using backend.domain.validation;
using Range = backend.domain.Limits.Range;

namespace backend.partners
{
	public class MaltaShip : PartnerBase
	{
		public MaltaShip(IMapper mapper, IRequestValidator requestValidator) : base(
			mapper: mapper,
			requestValidator: requestValidator,
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