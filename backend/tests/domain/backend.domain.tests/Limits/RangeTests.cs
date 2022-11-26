using System;
using System.Collections.Generic;
using backend.domain.Limits;
using NUnit.Framework;
using Range = backend.domain.Limits.Range;

namespace backend.domain.tests.Limits
{
	public class RangeTests
	{
		private static IEnumerable<object[]> IncludesScenarios()
		{
			// Both lower and upper limits
			yield return new object[] {
				new Range(
					lowerLimit: new Limit(10, true),
					upperLimit: new Limit(15, true)),
				9.99m, false
			};
			yield return new object[] {
				new Range(
					lowerLimit: new Limit(10, true),
					upperLimit: new Limit(15, true)),
				10m, true
			};
			yield return new object[] {
				new Range(
					lowerLimit: new Limit(10, true),
					upperLimit: new Limit(15, true)),
				15m, true
			};
			yield return new object[] {
				new Range(
					lowerLimit: new Limit(10, true),
					upperLimit: new Limit(15, false)),
				15m, false
			};
			yield return new object[] {
				new Range(
					lowerLimit: new Limit(10, true),
					upperLimit: new Limit(15, true)),
				15.01m, false
			};

			// Lower limit only
			yield return new object[] {
				new Range(
					lowerLimit: new Limit(10, true),
					upperLimit: null),
				9.99m, false
			};
			yield return new object[] {
				new Range(
					lowerLimit: new Limit(10, true),
					upperLimit: null),
				10m, true
			};
			yield return new object[] {
				new Range(
					lowerLimit: new Limit(10, false),
					upperLimit: null),
				10m, false
			};
			yield return new object[] {
				new Range(
					lowerLimit: new Limit(10, true),
					upperLimit: null),
				1000m, true
			};

			// Upper limit only
			yield return new object[] {
				new Range(
					lowerLimit: null,
					upperLimit: new Limit(15, true)),
				10m, true
			};
			yield return new object[] {
				new Range(
					lowerLimit: null,
					upperLimit: new Limit(15, true)),
				15m, true
			};
			yield return new object[] {
				new Range(
					lowerLimit: null,
					upperLimit: new Limit(15, false)),
				15m, false
			};
			yield return new object[] {
				new Range(
					lowerLimit: null,
					upperLimit: new Limit(15, true)),
				15.01m, false
			};
		}

		[Test, TestCaseSource(nameof(IncludesScenarios))]
		public void GivenARangeAndAValue_ThenIncludesReturnCorrectly(Range range, decimal value, bool expectation)
		{
			// Act
			var result = range.Includes(value);

			// Assert
			Assert.AreEqual(expectation, result);
		}

		[Test]
		public void GivenARangeWithBothLimitsNull_ThenConstructorThrows()
		{
			// Arrange
			// Act
			// Assert
			var exception = Assert.Throws<ArgumentException>(() => new Range(null, null));
			Assert.AreEqual("At least one limit should have a value", exception.Message);
		}

		private static IEnumerable<object[]> GetMessageScenarios()
		{
			yield return new object[]
			{
				new Range(
					lowerLimit: new Limit(10, true),
					upperLimit: new Limit(15, true)),
				"should be >= 10 and <= 15"
			};

			yield return new object[]
			{
				new Range(
					lowerLimit: new Limit(10, false),
					upperLimit: new Limit(15, true)),
				"should be > 10 and <= 15"
			};

			yield return new object[]
			{
				new Range(
					lowerLimit: null,
					upperLimit: new Limit(15, false)),
				"should be < 15"
			};

			yield return new object[]
			{
				new Range(
					lowerLimit: new Limit(10, true),
					upperLimit: null),
				"should be >= 10"
			};
		}
		[Test, TestCaseSource(nameof(GetMessageScenarios))]
		public void GivenARange_ThenMessageIsCorrect(Range range, string expectedMessage)
		{
			// Act
			var message = range.GetMessage();

			// Assert
			Assert.AreEqual(expectedMessage, message);
		}
	}
}
