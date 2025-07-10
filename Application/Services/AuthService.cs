using System.Security.Claims;
using System.Text;
using Application.Dto.Authentication;
using Application.Exceptions;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class AuthService: IAuthService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;
    private readonly ILogger<AuthService> _logger;
    
    public AuthService(UserManager<ApplicationUser> userManager, IMapper mapper, IConfiguration config, SignInManager<ApplicationUser> signInManager, ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _mapper = mapper;
        _config = config;
        _signInManager = signInManager;
        _logger = logger;
    }

    public async Task CreateAdminAsync(RegisterAdminRequest request)
    {
        _logger.LogInformation("Creating admin attempt for username: {Username}", request.Username);
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
        _logger.LogInformation("Admin created successfully. username: {Username}", request.Username);
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        _logger.LogInformation("Login attempt for username: {Username}", request.Username);
        var user = await _userManager.FindByNameAsync(request.Username);
        if (user == null)
        {
            _logger.LogWarning("Login failed. Username not found: {Username}", request.Username);
            throw new ValidationException("نام کاربری یا رمز عبور اشتباه است");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
        {
            _logger.LogWarning("Login failed. Wrong password for username: {Username}", request.Username);
            throw new ValidationException("نام کاربری یا رمز عبور اشتباه است");
        }

        var token = GenerateJwtToken(user);
        return token;
    }
    
    private LoginResponse GenerateJwtToken(ApplicationUser user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName!)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow.AddDays(1);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: credentials);

        return new LoginResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expires = expires
        };
    }
}