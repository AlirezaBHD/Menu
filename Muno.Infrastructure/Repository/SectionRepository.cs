using Muno.Domain.Entities.Sections;
using Muno.Domain.Interfaces.Repositories;
using Muno.Domain.Interfaces.Services;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class SectionRepository : Repository<Section>, ISectionRepository
{
    #region Injection

    private readonly ApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;

    protected override IQueryable<Section> LimitedQuery
    {
        get
        {
            IQueryable<Section> query = base.LimitedQuery;
            
            query = query
                .Include(s => s.Category)
                .ThenInclude(c => c!.Restaurant)
                .Where(s => s.Category!.RestaurantId == _currentUser.RestaurantId);
            
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