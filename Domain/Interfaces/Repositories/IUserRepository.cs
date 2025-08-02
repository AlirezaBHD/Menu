using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository: IRepository<User>
{
    Task<User?> FindByUsernameAsync(string requestUsername);
    Task<bool> AddUserAsync(User user);
}