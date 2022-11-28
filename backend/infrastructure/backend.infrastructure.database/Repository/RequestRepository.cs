using System.Diagnostics.CodeAnalysis;
using backend.infrastructure.database.entities.Request;

namespace backend.infrastructure.database.Repository
{
	[ExcludeFromCodeCoverage]
	public class RequestRepository : IRequestRepository
	{
		private readonly RequestDb _requestDb;

		public RequestRepository(RequestDb requestDb)
		{
			this._requestDb = requestDb;
		}

		public async Task SaveAsync(Request request)
		{
			_requestDb.Requests.Add(request);
			await _requestDb.SaveChangesAsync();
		}

		public async Task<IEnumerable<Request>> GetAllAsync()
		{
			return _requestDb.Requests;
		}
	}
}
