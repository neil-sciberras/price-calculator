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

		public PersistedDataService(IMapper mapper, IRequestRepository requestRepository)
		{
			_mapper = mapper;
			_requestRepository = requestRepository;
		}

		public async Task<IEnumerable<Request>> GetAllAsync()
		{
			var entities = await _requestRepository.GetAllAsync();

			return entities.Select(e => _mapper.Map<Request>(e));
		}

		public async Task SaveAsync(PriceRequest priceRequest)
		{
			var entity = _mapper.Map<infrastructure.database.entities.Request.Request>(priceRequest);
			entity.Date = DateTime.UtcNow;

			await _requestRepository.SaveAsync(entity);
		}
	}
}
