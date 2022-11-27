using backend.infrastructure.database.entities.Request;

namespace backend.infrastructure.database.Repository
{
	public class RequestRepository : IRequestRepository
	{
		public async Task SaveAsync(Request request)
		{
			await using var db = new RequestDb();
			db.Requests.Add(request);
			await db.SaveChangesAsync();
		}

		public async Task<IEnumerable<Request>> GetAllAsync()
		{
			await using var db = new RequestDb();
			return db.Requests;
		}
	}
}
