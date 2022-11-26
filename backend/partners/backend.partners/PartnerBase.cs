using AutoMapper;
using backend.api.contracts;
using backend.api.contracts.Request;
using backend.domain.validation;
using backend.partners.interfaces;
using backend.utilities;
using Range = backend.domain.Limits.Range;

namespace backend.partners
{
	public abstract class PartnerBase : IPartner
	{
		private readonly IMapper _mapper;
		private readonly IRequestValidator _requestValidator;
		private readonly Range _weightRange;
		private readonly Range _volumeRange;
		
		protected PartnerBase(IMapper mapper, IRequestValidator requestValidator, Range weightRange, Range volumeRange)
		{
			_mapper = mapper.NotNull(nameof(mapper));
			_requestValidator = requestValidator.NotNull(nameof(requestValidator));
			_weightRange = weightRange.NotNull(nameof(weightRange));
			_volumeRange = volumeRange.NotNull(nameof(volumeRange));
		}

		public ValidationResult Validate(PriceRequest priceRequest)
		{
			var domainRequest = _mapper.Map<domain.Request.PriceRequest>(priceRequest);
			var result = _requestValidator.Validate(domainRequest, _weightRange, _volumeRange);

			return _mapper.Map<ValidationResult>(result);
		}

		public decimal Calculate(PriceRequest priceRequest)
		{
			throw new NotImplementedException();
		}
	}
}
