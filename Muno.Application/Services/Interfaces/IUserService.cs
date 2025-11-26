using Muno.Domain.Entities;
using Muno.Application.Dto.User;

namespace Muno.Application.Services.Interfaces;

public interface IUserService: IService<User>
{
    Task<IEnumerable<UserRestaurantsDto>> Restaurants();
    
    Task SetRestaurantIdInSessionAsync(int restaurantId);
    
    Task<UserCredentialsDto?> FindUserByUsernameOrEmailAsync(string username, string email);
}