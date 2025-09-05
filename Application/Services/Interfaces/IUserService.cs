using Application.Dto.User;
using Domain.Entities;

namespace Application.Services.Interfaces;

public interface IUserService: IService<User>
{
    Task<IEnumerable<UserRestaurantsDto>> Restaurants();
    Task SetRestaurantIdInSessionAsync(int restaurantId);
    Task<UserCredentialsDto?> FindUserByUsernameOrEmailAsync(string username, string email);
}