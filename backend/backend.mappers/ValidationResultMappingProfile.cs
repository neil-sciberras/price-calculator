using AutoMapper;
using backend.domain;

namespace backend.mappers
{
	public class ValidationResultMappingProfile : Profile
	{
		public ValidationResultMappingProfile()
		{
			CreateMap<ValidationResult, api.contracts.ValidationResult>();
		}
	}
}
