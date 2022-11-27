using backend.api.contracts.Request;
using backend.application.interfaces;
using backend.partners.interfaces;
using static System.String;

namespace backend.application
{
	public class Handler : IHandler
	{
		private readonly IEnumerable<IPartner> _partners;
		
		public Handler(IEnumerable<IPartner> partners)
		{
			_partners = partners;
		}

		public decimal Handle(PriceRequest priceRequest)
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
				throw new Exception(Join("\n", validationErrorMessages));
			}

			return prices.Min();
		}
	}
}
