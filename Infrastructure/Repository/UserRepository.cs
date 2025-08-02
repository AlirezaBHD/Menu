using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Persistence;

namespace Infrastructure.Repository;

public class UserRepository:Repository<User>, IUserRepository
{
    #region Injection
    
    private readonly ApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;

    public UserRepository(ApplicationDbContext context, ICurrentUser currentUser) : base(context)
    {
        _context = context;
        _currentUser = currentUser;
    }
    
    #endregion
}