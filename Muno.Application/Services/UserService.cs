using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Muno.Application.Dto.User;
using Muno.Application.Services.Interfaces;

namespace Muno.Application.Services;

public class UserService : Service<User>, IUserService
{
    #region Injection

    private readonly IUserRepository _userRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ICurrentLanguage _currentLanguage;

    public UserService(IMapper mapper, ILogger<User> logger, IUserRepository userRepository,
        ICurrentUser currentUser, IHttpContextAccessor contextAccessor, ICurrentLanguage currentLanguage)
        : base(mapper, userRepository, logger)
    {
        _userRepository = userRepository;
        _currentUser = currentUser;
        _contextAccessor = contextAccessor;
        _currentLanguage = currentLanguage;
    }

    #endregion

    public async Task<IEnumerable<UserRestaurantsDto>> Restaurants()
    {
        var lang = _currentLanguage.GetLanguage();

        var result = await Queryable
            .Where(u => u.Id == _currentUser.UserId)
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
        var isOwnedByUser = await _userRepository.GetQueryable()
            .Where(u => u.Id == _currentUser.UserId)
            .SelectMany(u => u.Restaurants)
            .AnyAsync(r => r.Id == restaurantId);

        if (isOwnedByUser)
        {
            _contextAccessor.HttpContext!.Session.SetString("CurrentRestaurantId", restaurantId.ToString());
        }
    }

    public async Task<UserCredentialsDto?> FindUserByUsernameOrEmailAsync(string username, string email)
    {
        var normalizedUsername = username.ToUpperInvariant();
        var normalizedEmail = email.ToUpperInvariant();


        var userCredentials = Queryable.Where(u =>
                u.NormalizedUsername == normalizedUsername ||
                u.NormalizedEmail == normalizedEmail)
            .AsNoTracking()
            .Select(u => new UserCredentialsDto()
            {
                Username = username,
                Email = email
            }).FirstOrDefault();

        return userCredentials;
    }
}