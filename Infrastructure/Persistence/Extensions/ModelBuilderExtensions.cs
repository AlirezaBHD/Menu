using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Extensions;

public static class ModelBuilderExtensions
{
    public static void ConfigureActivityPeriod<T>(this OwnedNavigationBuilder<T, ActivityPeriod> ap)
        where T : class
    {
        ap.Property(p => p.IsActive).HasColumnName("is_active");
        ap.Property(p => p.ActivityType).HasColumnName("activity_type");
        ap.Property(p => p.FromTime).HasColumnName("from_time");
        ap.Property(p => p.ToTime).HasColumnName("to_time");
    }
}
