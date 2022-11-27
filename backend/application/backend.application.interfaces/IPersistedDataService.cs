using backend.api.contracts.PersistedData;
using backend.api.contracts.Request;

namespace backend.application.interfaces
{
	public interface IPersistedDataService
	{
		Task<IEnumerable<Request>> GetAllAsync();
		Task SaveAsync(PriceRequest request);
	}
}
