using AutoMapper;
using backend.api.contracts.PersistedData;
using backend.api.contracts.Request;
using backend.application.interfaces;
using backend.infrastructure.database.Repository;

namespace backend.application
{
	public class PersistedDataService : IPersistedDataService
	{
		private readonly IMapper _mapper;
		private readonly IRequestRepository _requestRepository;
		private readonly IDateService _dateService;

		public PersistedDataService(IMapper mapper, IRequestRepository requestRepository, IDateService dateService)
		{
			_mapper = mapper;
			_requestRepository = requestRepository;
			_dateService = dateService;
		}

		public async Task<IEnumerable<Request>> GetAllAsync()
		{
			var entities = await _requestRepository.GetAllAsync();

			return entities.Select(e => _mapper.Map<Request>(e));
		}

		public async Task SaveAsync(PriceRequest priceRequest)
		{
			var entity = _mapper.Map<infrastructure.database.entities.Request.Request>(priceRequest);
			entity.Date = _dateService.GetDate();

			await _requestRepository.SaveAsync(entity);
		}
	}
}
