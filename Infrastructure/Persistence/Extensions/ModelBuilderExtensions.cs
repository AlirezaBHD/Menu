using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Extensions;

public static class ModelBuilderExtensions
{
    public static void ConfigureAvailabilityPeriod<T>(this OwnedNavigationBuilder<T, AvailabilityPeriod> ap)
        where T : class
    {
        ap.Property(p => p.IsAvailable).HasColumnName("is_available");
        ap.Property(p => p.AvailabilityType).HasColumnName("availability_type");
        ap.Property(p => p.FromTime).HasColumnName("from_time");
        ap.Property(p => p.ToTime).HasColumnName("to_time");
    }
}
