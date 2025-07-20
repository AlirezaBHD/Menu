using Application.Dto.AvailabilityPeriod;
using Application.Extensions;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class AvailabilityPeriodProfile : Profile
{
    public AvailabilityPeriodProfile()
    {
        CreateMap<AvailabilityPeriodRequest, AvailabilityPeriod>();
        CreateMap<AvailabilityPeriod, AvailabilityPeriodResponse>()
            .ForMember(dest => dest.AvailabilityTypeName,
                opt => opt.MapFrom(src => src.ActivityType.GetDisplayName()));
    }
}