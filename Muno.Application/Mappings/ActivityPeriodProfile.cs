using Muno.Application.Extensions;
using AutoMapper;
using Domain.Entities;
using Muno.Application.Dto.ActivityPeriod;

namespace Muno.Application.Mappings;

public class ActivityPeriodProfile : Profile
{
    public ActivityPeriodProfile()
    {
        CreateMap<ActivityPeriodRequest, ActivityPeriod>();
        CreateMap<ActivityPeriod, ActivityPeriodResponse>()
            .ForMember(dest => dest.ActivityTypeName,
                opt => opt.MapFrom(src => src.ActivityType.GetDisplayName()));
    }
}