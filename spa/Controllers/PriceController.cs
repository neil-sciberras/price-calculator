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
		private readonly IHandler _handler;

		public PriceController(IHandler handler)
		{
			_handler = handler.NotNull(nameof(handler));
		}

		[HttpPost]
		public decimal Post(PriceRequest priceRequest)
		{
			return _handler.Handle(priceRequest);
		}
	}
}