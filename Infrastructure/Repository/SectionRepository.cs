using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class SectionRepository:Repository<Section>, ISectionRepository
{
    #region Injection
    
    private readonly ApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;
    protected override IQueryable<Section> LimitedQuery
    {
        get
        {
            IQueryable<Section> query = base.LimitedQuery;

            if (_currentUser.UserId.HasValue)
            {
                query = query
                    .Include(s => s.Category)
                    .ThenInclude(c => c!.Restaurant)
                    .Where(s => s.Category!.Restaurant!.OwnerId == _currentUser.UserId);
            }

            return query;
        }
    }

    public SectionRepository(ApplicationDbContext context, ICurrentUser currentUser) : base(context)
    {
        _context = context;
        _currentUser = currentUser;
    }
    
    #endregion
}