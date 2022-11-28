using backend.api.contracts.Request;
using backend.application.interfaces;
using backend.domain.validation.Exceptions;
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
		public async Task<IActionResult> PostAsync([FromBody]PriceRequest priceRequest)
		{
			try
			{
				await _persistedDataService.SaveAsync(priceRequest);

				return Ok(_priceService.GetPrice(priceRequest));
			}
			catch (InvalidRequestException e)
			{
				return BadRequest(e.Message);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return StatusCode(500);
			}
		}
	}
}