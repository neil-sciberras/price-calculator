using AutoMapper;
using NUnit.Framework;

namespace backend.mappers.tests
{
	public class ValidationResultMappingProfileTests
	{
		[Test]
		public void ValidConfigurationTest()
		{
			var config = new MapperConfiguration(cfg => cfg.AddProfile<ValidationResultMappingProfile>());
			config.AssertConfigurationIsValid();
		}
	}
}