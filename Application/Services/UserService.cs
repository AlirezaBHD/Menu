using Application.Dto.User;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class UserService : Service<User>, IUserService
{
    #region Injection

    private readonly IUserRepository _userRepository;
    private readonly ICurrentUser _currentUser;
    private readonly IHttpContextAccessor _contextAccessor;

    public UserService(IMapper mapper, ILogger<User> logger, IUserRepository userRepository,
        ICurrentUser currentUser, IHttpContextAccessor contextAccessor)
        : base(mapper, userRepository, logger)
    {
        _userRepository = userRepository;
        _currentUser = currentUser;
        _contextAccessor = contextAccessor;
    }

    #endregion

    public async Task<IEnumerable<UserRestaurantsDto>> Restaurants()
    {
        var result = await Queryable
            .Where(u => u.Id == _currentUser.UserId)
            .SelectMany(u => u.Restaurants.Select(
                r => new UserRestaurantsDto
                {
                    Name = r.Name,
                    Id = r.Id
                })).AsNoTracking().ToListAsync();

        return result;
    }

    public async Task SetRestaurantIdInSessionAsync(Guid restaurantId)
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