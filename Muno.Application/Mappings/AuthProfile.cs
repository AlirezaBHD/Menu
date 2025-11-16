using AutoMapper;
using Domain.Entities;
using Muno.Application.Dto.Authentication;

namespace Muno.Application.Mappings;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<RegisterAdminRequest, User>()
            .ForMember(dest => dest.Username, opt =>
            opt.MapFrom(src => src.Username));
    }
}