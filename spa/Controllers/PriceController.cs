using System.ComponentModel.DataAnnotations;
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

		/// <summary>
		/// Gets the cheapest courier price
		/// </summary>
		/// <param name="priceRequest">The customer's request</param>
		/// <response code="200">Returns price</response>
		/// <response code="400">If the request is invalid (contains negative or zero values)</response>
		/// <response code="500">If an unhandled exception occurs</response>

		[ProducesResponseType(typeof(decimal), 200)]
		[ProducesResponseType(typeof(string), 400)]
		[ProducesResponseType(500)]
		[HttpPost]
		public async Task<IActionResult> PostAsync([FromBody, Required]PriceRequest priceRequest)
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