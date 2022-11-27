using System;
using AutoMapper;
using Moq;
using NUnit.Framework;

namespace backend.partners.tests
{
	public class PartnerFactoryTest
	{
		[Test]
		[TestCase(PartnerFactory.Cargo4You)]
		[TestCase(PartnerFactory.ShipFaster)]
		[TestCase(PartnerFactory.MaltaShip)]
		public void GivenAValidPartnerName_ThenFactoryReturns(string partnerName)
		{
			// Arrange
			var mapperMock = new Mock<IMapper>();

			// Act
			var partnerInstance = PartnerFactory.Create(mapperMock.Object, partnerName);

			// Assert
			Assert.NotNull(partnerInstance);
		}

		[Test]
		public void GivenAnInvalidPartnerName_ThenFactoryThrows()
		{
			// Arrange
			var mapperMock = new Mock<IMapper>();

			// Act
			var exception = Assert.Throws<Exception>(() => PartnerFactory.Create(mapperMock.Object, "dummy"));

			// Assert
			Assert.AreEqual("dummy is not a valid partner name. Valid partners are: cargo4you, shipfaster, maltaship", exception.Message);
		}
	}
}
