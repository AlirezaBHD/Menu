using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    private readonly ICurrentUser _currentUser;
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUser currentUser)
        : base(options)
    {
        _currentUser = currentUser;
    }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries<BaseEntity>()
            .Where(e => e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            entry.Entity.ModifiedOn = DateTime.UtcNow;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
    private Guid? CurrentUserId => _currentUser.UserId;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var baseEntityType = typeof(BaseEntity);
        var entityTypes = modelBuilder.Model.GetEntityTypes()
            .Where(t => t.ClrType is { IsClass: true, IsAbstract: false } && baseEntityType.IsAssignableFrom(t.ClrType));

        foreach (var entityType in entityTypes)
        {
            modelBuilder.Entity(entityType.ClrType)
                .Property(nameof(BaseEntity.Id))
                .HasDefaultValueSql("gen_random_uuid()");
        }

        modelBuilder.Entity<Restaurant>()
            .HasQueryFilter(r => r.OwnerId == CurrentUserId || CurrentUserId == null);
        
        modelBuilder.Entity<Category>()
            .Navigation(c => c.Restaurant)
            .AutoInclude();
        
        modelBuilder.Entity<Category>()
            .HasQueryFilter(c => c.Restaurant!.OwnerId == CurrentUserId || CurrentUserId == null);
        
        modelBuilder.Entity<Section>()
            .Navigation(s => s.Category)
            .AutoInclude();
        
        modelBuilder.Entity<Section>()
            .HasQueryFilter(s => s.Category!.RestaurantId == CurrentUserId || CurrentUserId == null);
        
        modelBuilder.Entity<MenuItem>()
            .Navigation(m => m.Section)
            .AutoInclude();
        
        modelBuilder.Entity<MenuItem>()
            .HasQueryFilter(m => m.Section!.Category!.Restaurant!.OwnerId == CurrentUserId || CurrentUserId == null);

    }
    
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Section> Sections => Set<Section>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
}
