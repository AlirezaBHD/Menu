using Muno.Domain.Entities;
using Muno.Domain.Interfaces.Repositories;
using Muno.Domain.Interfaces.Services;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    #region Injection

    private readonly ApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(ApplicationDbContext context, ICurrentUser currentUser, ILogger<UserRepository> logger) :
        base(context)
    {
        _context = context;
        _currentUser = currentUser;
        _logger = logger;
    }

    #endregion

    public async Task<User?> FindByUsernameAsync(string requestUsername)
    {
        var queryable = GetQueryable();

        var user = await queryable
            .Include(u => u.Roles)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.NormalizedUsername == requestUsername.ToUpper());
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
            _logger.LogError(e, e.Message);
            return false;
        }
    }
}