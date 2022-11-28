using backend.api.contracts.Request;
using backend.application.interfaces;
using backend.domain.validation.Exceptions;
using backend.partners.interfaces;
using static System.String;

namespace backend.application
{
	public class PriceService : IPriceService
	{
		private readonly IEnumerable<IPartner> _partners;
		
		public PriceService(IEnumerable<IPartner> partners)
		{
			_partners = partners;
		}

		public decimal GetPrice(PriceRequest priceRequest)
		{
			var prices = new List<decimal>();
			var validationErrorMessages = new List<string>();

			foreach (var partner in _partners)
			{
				var validationResult = partner.Validate(priceRequest);

				if (validationResult.Success)
				{
					prices.Add(partner.Calculate(priceRequest));
				}
				else
				{
					validationErrorMessages.Add(validationResult.Message);
				}
			}

			if (!prices.Any())
			{
				throw new InvalidRequestException(Join("\n", validationErrorMessages));
			}

			return prices.Min();
		}
	}
}
