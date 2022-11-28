using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using backend.api.contracts.Request;
using backend.application.interfaces;
using backend.infrastructure.database.entities.Request;
using backend.infrastructure.database.Repository;
using Moq;
using NUnit.Framework;

namespace backend.application.tests
{
	public class PersistedDataServiceTests
	{
		private Mock<IRequestRepository> _repositoryMock;
		private Mock<IMapper> _mapperMock;
		private Mock<IDateService> _dateServiceMock;

		private IPersistedDataService GetService() => new PersistedDataService(_mapperMock.Object, _repositoryMock.Object, _dateServiceMock.Object);

		[SetUp]
		public void Setup()
		{
			_repositoryMock = new Mock<IRequestRepository>();
			_mapperMock = new Mock<IMapper>();
			_dateServiceMock = new Mock<IDateService>();
		}

		[Test]
		public async Task GivenRepositoryReturns_ThenGetAllReturns()
		{
			// Arrange
			_repositoryMock
				.Setup(mock => mock.GetAllAsync())
				.ReturnsAsync(new List<Request>() { new Request(), new Request() });

			_mapperMock
				.Setup(mock => mock.Map<api.contracts.PersistedData.Request>(It.IsAny<Request>()))
				.Returns(new api.contracts.PersistedData.Request());

			var service = GetService();

			// Act
			var result = await service.GetAllAsync();

			// Assert
			Assert.AreEqual(2, result.Count());
		}

		[Test]
		public async Task GivenARequest_ThenSaveAsyncCallsRepository()
		{
			// Arrange
			_mapperMock
				.Setup(mock => mock.Map<Request>(It.IsAny<PriceRequest>()))
				.Returns(new Request());

			var service = GetService();

			// Act
			await service.SaveAsync(new PriceRequest());

			// Assert
			_mapperMock.Verify(mock => mock.Map<Request>(It.IsAny<PriceRequest>()), Times.Once);
			_dateServiceMock.Verify(mock => mock.GetDate(), Times.Once);
			_repositoryMock.Verify(mock => mock.SaveAsync(It.IsAny<Request>()), Times.Once);
		}
	}
}
