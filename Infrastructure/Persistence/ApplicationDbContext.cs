using Domain.Entities;
using Domain.Entities.MenuItem;
using Domain.Localization;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

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
                .ValueGeneratedOnAdd();
        }
        
        modelBuilder.Entity<Category>()
            .OwnsOne(c => c.ActivityPeriod)
            .ConfigureActivityPeriod();

        modelBuilder.Entity<Section>()
            .OwnsOne(s => s.ActivityPeriod)
            .ConfigureActivityPeriod();

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasMany(m => m.Variants)
                .WithOne(v => v.MenuItem)
                .HasForeignKey(v => v.MenuItemId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(m => m.Section)
                .WithMany(s => s.MenuItems)
                .HasForeignKey(m => m.SectionId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(l => l.Code);
        });
        
        modelBuilder.Entity<MenuItem>()
            .OwnsOne(m => m.ActivityPeriod)
            .ConfigureActivityPeriod();
        
        modelBuilder.Entity<MenuItemTranslation>(entity =>
        {
            entity.HasOne(mt => mt.MenuItem)
                .WithMany(m => m.Translations)
                .HasForeignKey(mt => mt.MenuItemId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(mt => mt.Language)
                .WithMany()
                .HasForeignKey(mt => mt.LanguageCode)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(mt => new { mt.MenuItemId, mt.LanguageCode })
                .IsUnique();
        });
        
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.HasIndex(u => u.NormalizedUsername).IsUnique();
            entity.HasIndex(u => u.NormalizedEmail).IsUnique();
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.HasIndex(r => r.NormalizedName).IsUnique();
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(ur => new { ur.UserId, ur.RoleId });

            entity.HasOne(ur => ur.User)
                .WithMany(u => u.Roles)
                .HasForeignKey(ur => ur.UserId);

            entity.HasOne(ur => ur.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(ur => ur.RoleId);
        });
        
        modelBuilder.Entity<Language>().HasData(SupportedLanguages.All);
    }
    
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Section> Sections => Set<Section>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<MenuItemTranslation> MenuItemTranslations => Set<MenuItemTranslation>();
    public DbSet<MenuItemVariant> MenuItemVariant => Set<MenuItemVariant>();
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Language> Languages { get; set; }
}