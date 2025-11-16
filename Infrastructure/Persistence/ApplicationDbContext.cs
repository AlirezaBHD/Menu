using Muno.Domain.Entities;
using Muno.Domain.Entities.Categories;
using Muno.Domain.Entities.MenuItems;
using Muno.Domain.Entities.MenuItemVariants;
using Muno.Domain.Entities.Restaurants;
using Muno.Domain.Entities.Sections;
using Muno.Domain.Localization;
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
        
        modelBuilder.Entity<Restaurant>()
            .OwnsOne(r => r.ActivityPeriod)
            .ConfigureActivityPeriod();
        
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
            entity.HasOne(mt => mt.Core)
                .WithMany(m => m.Translations)
                .HasForeignKey(mt => mt.CoreId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(mt => mt.Language)
                .WithMany()
                .HasForeignKey(mt => mt.LanguageCode)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(mt => new { mt.CoreId, mt.LanguageCode })
                .IsUnique();
        });
        
        
        modelBuilder.Entity<CategoryTranslation>(entity =>
        {
            entity.HasOne(ct => ct.Core)
                .WithMany(c => c.Translations)
                .HasForeignKey(ct => ct.CoreId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(mt => mt.Language)
                .WithMany()
                .HasForeignKey(mt => mt.LanguageCode)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(ct => new { ct.CoreId, ct.LanguageCode })
                .IsUnique();
        });
        
        modelBuilder.Entity<MenuItemVariantTranslation>(entity =>
        {
            entity.HasOne(vt => vt.Core)
                .WithMany(v => v.Translations)
                .HasForeignKey(ct => ct.CoreId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(vt => vt.Language)
                .WithMany()
                .HasForeignKey(vt => vt.LanguageCode)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(vt => new { vt.CoreId, vt.LanguageCode })
                .IsUnique();
        });
        
        
        modelBuilder.Entity<RestaurantTranslation>(entity =>
        {
            entity.HasOne(rt => rt.Core)
                .WithMany(r => r.Translations)
                .HasForeignKey(rt => rt.CoreId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(rt => rt.Language)
                .WithMany()
                .HasForeignKey(rt => rt.LanguageCode)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(rt => new { rt.CoreId, rt.LanguageCode })
                .IsUnique();
        });
        
        modelBuilder.Entity<SectionTranslation>(entity =>
        {
            entity.HasOne(st => st.Core)
                .WithMany(r => r.Translations)
                .HasForeignKey(st => st.CoreId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(st => st.Language)
                .WithMany()
                .HasForeignKey(st => st.LanguageCode)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(st => new { st.CoreId, st.LanguageCode })
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