using Application.Dto.Authentication;
using Application.Exceptions;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class AuthService: IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    
    public AuthService(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task CreateAdminAsync(RegisterAdminRequest request)
    {
        var existingUser = await _userManager.FindByNameAsync(request.Username);
        if (existingUser != null)
        {
            throw new ValidationException("نام کاربری تکراری است");
        }
        var user = _mapper.Map<ApplicationUser>(request);
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            throw new ValidationException("در فرایند ثبت ادمین خطایی رخ داد");
        }
    }
}