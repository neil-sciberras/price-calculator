using backend.api.contracts.Request;
using backend.application.interfaces;
using backend.partners.interfaces;
using backend.utilities;

namespace backend.application
{
	public class Handler : IHandler
	{
		private readonly IPartner _partner;

		public Handler(IPartner partner)
		{
			_partner = partner.NotNull(nameof(partner));
		}

		public decimal Handle(PriceRequest priceRequest)
		{
			var validationResult = _partner.Validate(priceRequest);

			if (!validationResult.Success)
				throw new Exception(validationResult.Message);

			return _partner.Calculate(priceRequest);
		}
	}
}
