using System;
using System.Collections.Generic;
using backend.domain.calculation.CalculationLogic;
using backend.domain.Limits;
using NUnit.Framework;
using Range = backend.domain.Limits.Range;

namespace backend.domain.calculation.tests.CalculationLogic
{
	public class CalculationLogicTests
	{
		private IEnumerable<PriceBracket> _priceBracketList;
		private calculation.CalculationLogic.CalculationLogic _calculationLogic;

		[SetUp]
		public void Setup()
		{
			_priceBracketList = new List<PriceBracket>()
			{
				new PriceBracket(
					range: new Range(
						lowerLimit: null,
						upperLimit: new Limit(2, true)),
					price: 15m),
				new PriceBracket(
					range: new Range(
						lowerLimit: new Limit(15, false),
						upperLimit: new Limit(20, true)),
					price: 35m),
				new PriceBracket(
					range: new Range(
						lowerLimit: new Limit(2, false),
						upperLimit: new Limit(15, true)),
					price: 18m)
			};

			_calculationLogic = new calculation.CalculationLogic.CalculationLogic(_priceBracketList);
		}

		[Test]
		[TestCase(1.99, 15)]
		[TestCase(2, 15)]
		[TestCase(2.01, 18)]
		[TestCase(15, 18)]
		[TestCase(15.01, 35)]
		[TestCase(20, 35)]
		public void GivenAnUnorderedListOfBrackets_CalculateStillReturnsTheCorrectPrice(double value, int expectedPrice)
		{
			// Act
			var price = _calculationLogic.Calculate((decimal)value);

			// Assert
			Assert.AreEqual((decimal)expectedPrice, price);
		}

		[Test]
		public void GivenAValueOutsideAllBrackets_ThenCalculateThrows()
		{
			// Act
			// Assert
			var exception = Assert.Throws<Exception>(() => _calculationLogic.Calculate(20.1m));

			Assert.AreEqual("Requested value does not match any price bracket", exception.Message);
		}
	}
}
