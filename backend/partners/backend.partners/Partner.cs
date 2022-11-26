using AutoMapper;
using backend.api.contracts;
using backend.api.contracts.Request;
using backend.domain.calculation;
using backend.domain.validation;
using backend.partners.interfaces;
using backend.utilities;

namespace backend.partners
{
	public class Partner : IPartner
	{
		private readonly IMapper _mapper;
		private readonly IRequestValidator _requestValidator;
		private readonly ICalculator _calculator;

		public Partner(IMapper mapper, IRequestValidator requestValidator, ICalculator calculator)
		{
			_mapper = mapper.NotNull(nameof(mapper));
			_requestValidator = requestValidator.NotNull(nameof(requestValidator));
			_calculator = calculator.NotNull(nameof(calculator));
		}

		public ValidationResult Validate(PriceRequest priceRequest)
		{
			var domainRequest = _mapper.Map<domain.Request.PriceRequest>(priceRequest);

			var result = _requestValidator.Validate(domainRequest);

			return _mapper.Map<ValidationResult>(result);
		}

		public decimal Calculate(PriceRequest priceRequest)
		{
			var domainRequest = _mapper.Map<domain.Request.PriceRequest>(priceRequest);

			return _calculator.Calculate(domainRequest);
		}
	}
}
