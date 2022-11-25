using AutoMapper;
using backend.api.contracts;
using backend.api.contracts.Request;
using backend.partners.interfaces;
using backend.utilities;
using Domain = backend.domain.Request;
using Range = backend.partners.limits.Range;

namespace backend.partners
{
	public class Cargo4You : IPartner
	{
		private readonly IMapper _mapper;
		private readonly Range _weightRange;
		private readonly Range _volumeRange;
		
		public Cargo4You(IMapper mapper, Range weightRange, Range volumeRange)
		{
			_weightRange = weightRange;
			_volumeRange = volumeRange;
			_mapper = mapper.NotNull(nameof(mapper));
		}

		public decimal Calculate(PriceRequest priceRequest)
		{
			var domainRequest = _mapper.Map<Domain.PriceRequest>(priceRequest);


		}

		public ValidationResult Validate(PriceRequest priceRequest)
		{
			var domainRequest = _mapper.Map<Domain.PriceRequest>(priceRequest);

			var isWeightValid = _weightRange.Includes(domainRequest.Weight.Value);
			var isVolumeValid = _volumeRange.Includes(domainRequest.Dimensions.Volume);

			if (isVolumeValid && isWeightValid) return new ValidationResult(true, null);

			var volumeMessage = !isVolumeValid ? $"Volume {_volumeRange.GetMessage()}" : string.Empty;
			var weightMessage = !isWeightValid? $"Weight {_weightRange.GetMessage()}" : string.Empty;
				
			var message = string.Join(",", new List<string> { volumeMessage, weightMessage });

			return new ValidationResult(false, message);

		}
	}
}
