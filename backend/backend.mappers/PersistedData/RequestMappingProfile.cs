using AutoMapper;
using backend.api.contracts.Request;
using backend.infrastructure.database.entities.Request;

namespace backend.mappers.PersistedData
{
	public class RequestMappingProfile : Profile
	{
		public RequestMappingProfile()
		{
			CreateMap<Request, api.contracts.PersistedData.Request>().ReverseMap();

			CreateMap<PriceRequest, api.contracts.PersistedData.Request>()
				.ForMember(dest => dest.Id, opts => opts.Ignore())
				.ForMember(dest => dest.Date, opts => opts.Ignore())
				.ForMember(dest => dest.Weight, opts => opts.MapFrom(src => src.Weight))
				.ForMember(dest => dest.Width, opts => opts.MapFrom(src => src.Dimensions.Width))
				.ForMember(dest => dest.Height, opts => opts.MapFrom(src => src.Dimensions.Height))
				.ForMember(dest => dest.Depth, opts => opts.MapFrom(src => src.Dimensions.Depth));
		}
	}
}
