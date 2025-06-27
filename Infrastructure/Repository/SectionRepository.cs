using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repository;

public class SectionRepository:Repository<Section>, ISectionRepository
{
    private readonly ApplicationDbContext _context;

    public SectionRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}