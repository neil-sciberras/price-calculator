using AutoMapper;
using NUnit.Framework;

namespace backend.mappers.tests
{
	public class PriceRequestMappingProfileTests
	{
		[Test]
		public void ValidConfigurationTest()
		{
			var config = new MapperConfiguration(cfg => cfg.AddProfile<PriceRequestMappingProfile>());
			config.AssertConfigurationIsValid();
		}
	}
}