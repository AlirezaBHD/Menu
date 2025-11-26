using Muno.Domain.Entities.Sections;
using Muno.Domain.Interfaces.Repositories;
using Muno.Domain.Interfaces.Services;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class SectionRepository(ApplicationDbContext context, ICurrentUser currentUser)
    : Repository<Section>(context), ISectionRepository
{
    protected override IQueryable<Section> LimitedQuery
    {
        get
        {
            var query = base.LimitedQuery;
            
            query = query
                .Include(s => s.Category)
                .ThenInclude(c => c!.Restaurant)
                .Where(s => s.Category!.RestaurantId == currentUser.RestaurantId);
            
            return query;
        }
    }
}