using System;
using NUnit.Framework;

namespace backend.utilities.tests
{
	public class ArgCheckTests
	{
		[Test]
		public void GivenNullArgument_ThenNotNullThrows()
		{
			// Arrange
			var arg = (object)null;

			// Act
			// Assert
			var exception = Assert.Throws<ArgumentNullException>(() => arg.NotNull(nameof(arg)));
			Assert.AreEqual("Value cannot be null. (Parameter 'arg')", exception.Message);
		}

		[Test]
		public void GivenANonNullArgument_ThenNotNullReturnsArgument()
		{
			// Arrange
			var arg = new object();

			// Act
			var result = arg.NotNull(nameof(arg));

			// Assert
			Assert.NotNull(result);
		}
	}
}