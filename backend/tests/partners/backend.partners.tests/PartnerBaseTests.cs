using AutoMapper;
using backend.api.contracts;
using backend.api.contracts.Request;
using backend.domain.validation;
using Moq;
using NUnit.Framework;
using Range = backend.domain.Limits.Range;

namespace backend.partners.tests
{
	public class PartnerBaseTests
	{
		private Mock<IMapper> _mapperMock;
		private Mock<IRequestValidator> _requestValidatorMock;

		[SetUp]
		public void Setup()
		{
			_mapperMock = new Mock<IMapper>();
			_requestValidatorMock = new Mock<IRequestValidator>();
		}
		
		[Test]
		public void GivenARequest_ThenValidateMapsAndReturnsCorrectly()
		{
			// Arrange
			var request = new PriceRequest();

			_mapperMock
				.Setup(mock => mock.Map<domain.Request.PriceRequest>(It.IsAny<PriceRequest>()))
				.Returns(new domain.Request.PriceRequest(1m, new domain.Request.Dimensions(1m, 1m, 1m)));

			_mapperMock
				.Setup(mock => mock.Map<ValidationResult>(It.IsAny<domain.ValidationResult>()))
				.Returns(new ValidationResult(true, null));

			_requestValidatorMock
				.Setup(mock => mock.Validate(It.IsAny<domain.Request.PriceRequest>(), It.IsAny<Range>(), It.IsAny<Range>()))
				.Returns(new domain.ValidationResult(true, null));
			
			var partner = new Cargo4You(_mapperMock.Object, _requestValidatorMock.Object);

			// Act
			var result = partner.Validate(request);

			// Assert
			Assert.NotNull(result);
			
			_mapperMock.Verify(
				mock => mock.Map<domain.Request.PriceRequest>(It.IsAny<PriceRequest>()), 
				Times.Once);
			
			_requestValidatorMock.Verify(
				mock => mock.Validate(It.IsAny<domain.Request.PriceRequest>(), It.IsAny<Range>(), It.IsAny<Range>()), 
				Times.Once);
			
			_mapperMock.Verify(
				mock => mock.Map<ValidationResult>(It.IsAny<domain.ValidationResult>()), 
				Times.Once);
		}
	}
}