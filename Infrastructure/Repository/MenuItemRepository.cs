using Domain.Entities;
using Domain.RepositoryInterfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repository;

public class MenuItemRepository:Repository<MenuItem>, IMenuItemRepository
{
    private readonly ApplicationDbContext _context;

    public MenuItemRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}