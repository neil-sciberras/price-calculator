using backend.infrastructure.database.entities.Request;

namespace backend.infrastructure.database.Repository
{
	public interface IRequestRepository
	{
		Task SaveAsync(Request request);
		Task<IEnumerable<Request>> GetAllAsync();
	}
}
