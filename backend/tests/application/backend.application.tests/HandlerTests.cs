using System;
using backend.api.contracts;
using backend.api.contracts.Request;
using backend.partners.interfaces;
using Moq;
using NUnit.Framework;

namespace backend.application.tests
{
	public class HandlerTests
	{
		private Mock<IPartner> _partnerMock;

		[SetUp]
		public void Setup()
		{
			_partnerMock = new Mock<IPartner>();
		}

		[Test]
		public void GivenAValidRequest_ThenCalculateIsCalled()
		{
			// Arrange
			_partnerMock = SetupPartnerMock(_partnerMock, success: true);

			var handler = new Handler(_partnerMock.Object);

			// Act
			handler.Handle(new PriceRequest());

			// Assert
			_partnerMock.Verify(mock => mock.Calculate(It.IsAny<PriceRequest>()), Times.Once);
		}

		[Test]
		public void GivenAnInvalidRequest_ThenHandleThrows()
		{
			// Arrange
			const string message = "example message";
			_partnerMock = SetupPartnerMock(_partnerMock, success: false, message: message);

			var handler = new Handler(_partnerMock.Object);

			// Act
			// Assert
			var exception = Assert.Throws<Exception>(() => handler.Handle(new PriceRequest()));
			Assert.AreEqual(message, exception.Message);
		}

		private static Mock<IPartner> SetupPartnerMock(Mock<IPartner> partnerMock, bool success, string? message = null)
		{
			partnerMock
				.Setup(mock => mock.Validate(It.IsAny<PriceRequest>()))
				.Returns(new ValidationResult(success, message));

			return partnerMock;
		}
	}
}