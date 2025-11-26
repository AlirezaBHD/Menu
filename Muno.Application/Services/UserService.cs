using AutoMapper;
using Muno.Domain.Entities;
using Muno.Domain.Interfaces.Repositories;
using Muno.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Muno.Application.Dto.User;
using Muno.Application.Services.Interfaces;

namespace Muno.Application.Services;

public class UserService(
    IMapper mapper,
    ILogger<User> logger,
    IUserRepository userRepository,
    ICurrentUser currentUser,
    IHttpContextAccessor contextAccessor,
    ICurrentLanguage currentLanguage)
    : Service<User>(mapper, userRepository, logger), IUserService
{

    public async Task<IEnumerable<UserRestaurantsDto>> Restaurants()
    {
        var lang = currentLanguage.GetLanguage();

        var result = await Queryable
            .Where(u => u.Id == currentUser.UserId)
            .SelectMany(u => u.Restaurants.Select(r => new UserRestaurantsDto
            {
                Id = r.Id,
                Name = r.Translations
                           .Where(t => t.LanguageCode == lang)
                           .Select(t => t.Name)
                           .FirstOrDefault() 
                       ?? r.Translations.Select(t => t.Name).FirstOrDefault() 
                       ?? string.Empty,
                Order = r.Order,
                LogoPath = r.LogoPath
            }))
            .OrderBy(r => r.Order)
            .AsNoTracking()
            .ToListAsync();

        return result;
    }


    public async Task SetRestaurantIdInSessionAsync(int restaurantId)
    {
        var isOwnedByUser = await userRepository.GetQueryable()
            .Where(u => u.Id == currentUser.UserId)
            .SelectMany(u => u.Restaurants)
            .AnyAsync(r => r.Id == restaurantId);

        if (isOwnedByUser)
        {
            contextAccessor.HttpContext!.Session.SetString("CurrentRestaurantId", restaurantId.ToString());
        }
    }

    public async Task<UserCredentialsDto?> FindUserByUsernameOrEmailAsync(string username, string email)
    {
        var normalizedUsername = username.ToUpperInvariant();
        var normalizedEmail = email.ToUpperInvariant();


        var userCredentials = await Queryable.Where(u =>
                u.NormalizedUsername == normalizedUsername ||
                u.NormalizedEmail == normalizedEmail)
            .AsNoTracking()
            .Select(u => new UserCredentialsDto()
            {
                Username = username,
                Email = email
            }).FirstOrDefaultAsync();

        return userCredentials;
    }
}