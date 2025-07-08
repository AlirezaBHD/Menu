using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
{
    #region Injection

    private readonly ApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;

    protected override IQueryable<MenuItem> LimitedQuery =>
        base.LimitedQuery
            .Include(m => m.Section)
            .ThenInclude(s => s!.Category)
            .ThenInclude(c => c!.Restaurant)
            .Where(m => m.Section!.Category!.Restaurant!.OwnerId == _currentUser.UserId);

    public MenuItemRepository(ApplicationDbContext context, ICurrentUser currentUser) : base(context)
    {
        _context = context;
        _currentUser = currentUser;
    }
    
    #endregion
}