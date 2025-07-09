using Application.Dto.Authentication;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<RegisterAdminRequest, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt =>
            opt.MapFrom(src => src.Username));
    }
}