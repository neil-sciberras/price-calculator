using AutoMapper;
using backend.mappers.PersistedData;
using NUnit.Framework;

namespace backend.mappers.tests.PersistedData
{
	public class RequestMappingProfileTests
	{
		[Test]
		public void ValidConfigurationTest()
		{
			var config = new MapperConfiguration(cfg => cfg.AddProfile<RequestMappingProfile>());
			config.AssertConfigurationIsValid();
		}
	}
}
