using System;
using System.Collections.Generic;
using System.Linq;
using backend.domain.calculation.CalculationLogic;
using backend.domain.Limits;
using NUnit.Framework;
using Range = backend.domain.Limits.Range;

namespace backend.domain.calculation.tests.CalculationLogic
{
	public class CalculationLogicWithExtraRateTests
	{
		private IEnumerable<PriceBracket> _priceBracketList;

		[SetUp]
		public void Setup()
		{
			_priceBracketList = new List<PriceBracket>()
			{
				new PriceBracket(
					range: new Limits.Range(
						lowerLimit: null,
						upperLimit: new Limit(2, true)),
					price: 15m),
				new PriceBracket(
					range: new Limits.Range(
						lowerLimit: new Limit(15, false),
						upperLimit: new Limit(20, true)),
					price: 35m),
				new PriceBracket(
					range: new Range(
						lowerLimit: new Limit(2, false),
						upperLimit: new Limit(15, true)),
					price: 18m),
				new PriceBracket(
					range: new Range(
						lowerLimit: new Limit(20, false),
						upperLimit: null),
					price: 40m)
			};
		}

		[Test]
		[TestCase(1.99, 15)]
		[TestCase(2, 15)]
		[TestCase(2.01, 18)]
		[TestCase(15, 18)]
		[TestCase(15.01, 35)]
		[TestCase(20, 35)]
		[TestCase(20.01, 40.5)]
		[TestCase(25.01, 43)]
		public void GivenAValue_ThenCalculateReturnsTheCorrectPrice(double value, double expectedPrice)
		{
			// Arrange
			var calculationLogic = new CalculationLogicWithExtraRate(_priceBracketList, 0.5m);

			// Act
			var price = calculationLogic.Calculate((decimal)value);

			// Assert
			Assert.AreEqual((decimal)expectedPrice, price);
		}

		private static IEnumerable<object[]> InvalidRangeScenarios()
		{
			yield return new object[]
			{
				new Range(
					lowerLimit: null,
					upperLimit: new Limit(1, true))
			};

			yield return new object[]
			{
				new Range(
					lowerLimit: new Limit(1, false),
					upperLimit: new Limit(2, true))
			};
		}

		[Test, TestCaseSource(nameof(InvalidRangeScenarios))]
		public void GivenAPriceBracketListWithTheBiggestOneHavingIncorrectRange_ThenCalculateThrows(Range range)
		{
			// Arrange
			var priceBracketList = _priceBracketList.ToList();
			priceBracketList[^1] = new PriceBracket(range, 40m);

			// Act
			// Assert
			var exception = Assert.Throws<Exception>(() => new CalculationLogicWithExtraRate(priceBracketList, ratePerExtraKg: 1m));
			Assert.AreEqual("The last bracket should have the lower limit set (and only that)", exception.Message);
		}
	}
}
