using System.Security.Claims;
using System.Text;
using AutoMapper;
using Muno.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Muno.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Muno.Application.Dto.Authentication;
using Muno.Application.Exceptions;
using Muno.Application.Localization;
using Muno.Application.Services.Interfaces;

namespace Muno.Application.Services;

public class AuthService(
    IMapper mapper,
    IConfiguration config,
    ILogger<AuthService> logger,
    IUserRepository userRepository,
    IUserService userService)
    : IAuthService
{
    
    public async Task CreateAdminAsync(RegisterAdminRequest request)
    {
        logger.LogInformation("Creating admin attempt for username: {Username}", request.Username);

        var existingUser = await userService.FindUserByUsernameOrEmailAsync(request.Username, request.Email);

        if (existingUser is not null)
        {
            var duplicateFields = new List<string>();

            if (existingUser.Email == request.Email)
                duplicateFields.Add(Resources.Email);

            if (existingUser.Username == request.Username)
                duplicateFields.Add(Resources.Username);

            if (duplicateFields.Count != 0)
                throw new ValidationException(
                    $"{string.Join(Resources.And, duplicateFields)} {Resources.AlreadyTaken} ");
        }

        var isCreatedSuccessfully = await CreateUserAsync(request);
        if (!isCreatedSuccessfully)
        {
            throw new ValidationException(Resources.ErrorOccurredInRegisteration);
        }

        logger.LogInformation("Admin created successfully. username: {Username}", request.Username);
    }


    private async Task<bool> CreateUserAsync(RegisterAdminRequest request)
    {
        var user = mapper.Map<User>(request);

        user.NormalizedEmail = request.Email.ToUpper();
        user.NormalizedUsername = request.Username.ToUpper();

        var passwordHasher = new PasswordHasher<User>();
        var hashedPassword = passwordHasher.HashPassword(user, request.Password);
        user.PasswordHash = hashedPassword;

        var result = await userRepository.AddUserAsync(user);

        return result;
    }


    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        logger.LogInformation("Login attempt for username: {Username}", request.Username);
        var user = await userRepository.FindByUsernameAsync(request.Username);
        if (user == null)
        {
            logger.LogWarning("Login failed. Username not found: {Username}", request.Username);
            throw new ValidationException(Resources.WrongUsernameOrPassword);
        }

        var passwordHasher = new PasswordHasher<User>();
        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result != PasswordVerificationResult.Success)
        {
            logger.LogWarning("Login failed. Wrong password for username: {Username}", request.Username);
            throw new ValidationException(Resources.WrongUsernameOrPassword);
        }

        var token = GenerateJwtToken(user);

        var response = new LoginResponse
        {
            Token = token,
            Username = user.Username
        };
        return response;
    }


    private string GenerateJwtToken(User user)
    {
        var userRoles = user.Roles.Select(r => r.Role.Name);
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id.ToString()),
            new (ClaimTypes.Name, user.Username)
        };

        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = DateTime.UtcNow.AddDays(1);

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}