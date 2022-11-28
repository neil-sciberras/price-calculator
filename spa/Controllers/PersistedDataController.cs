using backend.api.contracts.PersistedData;
using backend.application.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace spa.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PersistedDataController : ControllerBase
	{
		private readonly IPersistedDataService _persistedDataService;

		public PersistedDataController(IPersistedDataService persistedDataService)
		{
			_persistedDataService = persistedDataService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			try
			{
				return Ok(await _persistedDataService.GetAllAsync());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return StatusCode(500);
			}
		}
	}
}
