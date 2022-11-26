using backend.domain.calculation.CalculationLogic;
using backend.domain.Request;
using Moq;
using NUnit.Framework;

namespace backend.domain.calculation.tests
{
	public class Tests
	{
		private Mock<ICalculationLogic> _weightCalculationLogicMock;
		private Mock<ICalculationLogic> _volumeCalculationLogicMock;

		[SetUp]
		public void Setup()
		{
			_weightCalculationLogicMock = new Mock<ICalculationLogic>();
			_volumeCalculationLogicMock = new Mock<ICalculationLogic>();
		}

		[Test]
		[TestCase(10, 15, 15)]
		[TestCase(10, 10, 10)]
		public void GivenTwoPrices_ThenCalculateReturnsTheGreater(int weightPrice, int volumePrice, int expectedPrice)
		{
			// Arrange
			_weightCalculationLogicMock
				.Setup(mock => mock.Calculate(It.IsAny<decimal>()))
				.Returns(weightPrice);

			_volumeCalculationLogicMock
				.Setup(mock => mock.Calculate(It.IsAny<decimal>()))
				.Returns(volumePrice);

			var request = new PriceRequest(1m, new Dimensions(1m, 1m, 1m));

			var calculator = new Calculator(_weightCalculationLogicMock.Object, _volumeCalculationLogicMock.Object);
			
			// Act
			var price = calculator.Calculate(request);

			// Assert
			Assert.AreEqual(expectedPrice, price);
		}
	}
}