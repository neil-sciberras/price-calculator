using backend.domain.Request;
using NUnit.Framework;

namespace backend.domain.tests.Request
{
	public class DimensionsTests
	{
		[Test]
		public void GivenADimension_ThenVolumeIsCorrect()
		{
			// Arrange
			var dimension = new Dimensions(20m, 10.5m, 2);

			// Act
			var volume= dimension.Volume;

			// Assert
			Assert.AreEqual(20m*10.5m*2m, volume);
		}
	}
}
