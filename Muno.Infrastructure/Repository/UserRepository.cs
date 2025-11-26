using Muno.Domain.Entities;
using Muno.Domain.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository;

public class UserRepository(ApplicationDbContext context, ILogger<UserRepository> logger)
    : Repository<User>(context), IUserRepository
{

    public async Task<User?> FindByUsernameAsync(string requestUsername)
    {
        var queryable = GetQueryable();

        var user = await queryable
            .Include(u => u.Roles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.NormalizedUsername.Equals(requestUsername, StringComparison.CurrentCultureIgnoreCase));
        return user;
    }

    public async Task<bool> AddUserAsync(User user)
    {
        try
        {
            await AddAsync(user);
            await SaveAsync();
            return true;
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            return false;
        }
    }
}