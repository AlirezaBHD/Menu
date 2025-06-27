using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repository;

public class CategoryRepository:Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}