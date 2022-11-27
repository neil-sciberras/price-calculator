using System;
using System.Collections.Generic;
using backend.api.contracts;
using backend.api.contracts.Request;
using backend.partners.interfaces;
using Moq;
using NUnit.Framework;

namespace backend.application.tests
{
	public class PriceServiceTests
	{
		private static IEnumerable<object[]> PartnerScenarios()
		{
			yield return new object[]
			{
				new List<IPartner>
				{
					SetupPartnerMock(success: true, returnedPrice: 1).Object,
					SetupPartnerMock(success: true, returnedPrice: 2).Object,
					SetupPartnerMock(success: true, returnedPrice: 3).Object
				}, 1
			};

			yield return new object[]
			{
				new List<IPartner>
				{
					SetupPartnerMock(success: false, message: "error message").Object,
					SetupPartnerMock(success: true, returnedPrice: 2).Object,
					SetupPartnerMock(success: true, returnedPrice: 3).Object
				}, 2
			};
		}

		[Test, TestCaseSource(nameof(PartnerScenarios))]
		public void GivenAtLeastOnePartnerReturnsSuccess_ThenCalculateReturnsMinimumPrice(
			IEnumerable<IPartner> partnerList, int expectedPrice)
		{
			// Arrange
			var priceService = new PriceService(partnerList);

			// Act
			var result = priceService.GetPrice(new PriceRequest());

			// Assert
			Assert.AreEqual((decimal)expectedPrice, result);
		}

		[Test]
		public void GivenAllPartnersReturnFailedValidations_ThenHandleThrows()
		{
			// Arrange
			var priceService = new PriceService(new List<IPartner>
			{
				SetupPartnerMock(success: false, message: "error message 1").Object,
				SetupPartnerMock(success: false, message: "error message 2").Object,
				SetupPartnerMock(success: false, message: "error message 3").Object
			});

			// Act
			// Assert
			var exception = Assert.Throws<Exception>(() => priceService.GetPrice(new PriceRequest()));
			Assert.AreEqual("error message 1\nerror message 2\nerror message 3", exception.Message);
		}

		private static Mock<IPartner> SetupPartnerMock(bool success, decimal? returnedPrice = null, string? message = null)
		{
			var partnerMock = new Mock<IPartner>();

			partnerMock
				.Setup(mock => mock.Validate(It.IsAny<PriceRequest>()))
				.Returns(new ValidationResult(success, message));

			if (returnedPrice != null)
			{
				partnerMock
					.Setup(mock => mock.Calculate(It.IsAny<PriceRequest>()))
					.Returns(returnedPrice.Value);
			}

			return partnerMock;
		}
	}
}