using backend.api.contracts.Request;
using backend.application.interfaces;
using backend.utilities;
using Microsoft.AspNetCore.Mvc;

namespace spa.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PriceController : ControllerBase
	{
		private readonly IPriceService _priceService;
		private readonly IPersistedDataService _persistedDataService;

		public PriceController(IPriceService priceService, IPersistedDataService persistedDataService)
		{
			_persistedDataService = persistedDataService;
			_priceService = priceService.NotNull(nameof(priceService));
		}

		[HttpPost]
		public async Task<decimal> PostAsync([FromBody]PriceRequest priceRequest)
		{
			await _persistedDataService.SaveAsync(priceRequest);

			return _priceService.GetPrice(priceRequest);
		}
	}
}