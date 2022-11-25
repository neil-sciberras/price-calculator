using AutoMapper;
using Contracts = backend.api.contracts.Request;
using Domain = backend.domain.Request;

namespace backend.mappers
{
	internal class PriceRequestMappingProfile : Profile
	{
		public PriceRequestMappingProfile()
		{
			CreateMap<Contracts.Dimensions, Domain.Dimensions>();
			CreateMap<Contracts.Weight, Domain.Weight>();
			CreateMap<Contracts.PriceRequest, Domain.PriceRequest>();
		}
	}
}
