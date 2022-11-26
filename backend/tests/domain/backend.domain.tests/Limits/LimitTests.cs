using System.Collections.Generic;
using backend.domain.Limits;
using NUnit.Framework;

namespace backend.domain.tests.Limits
{
	public class LimitTests
	{
		private static IEnumerable<object[]> TestLimits()
		{
			yield return new object[] { new Limit(20, false), " 20" };
			yield return new object[] { new Limit(20, true), "= 20" };
		}

		[Test, TestCaseSource(nameof(TestLimits))]
		public void GivenALimit_ThenMessageIsReturnedCorrectly(Limit limit, string expectedMessage)
		{
			// Act
			var message = limit.GetMessage();

			// Assert
			Assert.AreEqual(expectedMessage, message);
		}
	}
}