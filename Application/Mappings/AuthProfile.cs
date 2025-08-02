using Application.Dto.Authentication;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings;

public class AuthProfile : Profile
{
    public AuthProfile()
    {
        CreateMap<RegisterAdminRequest, User>()
            .ForMember(dest => dest.Username, opt =>
            opt.MapFrom(src => src.Username));
    }
}