using Domain.Common.Attributes;
using Domain.Entities.Restaurants;
using Domain.Localization;

namespace Domain.Entities;

public class User : BaseEntity
{
    [LocalizeDisplay(nameof(Resources.Username))]
    public string Username { get; set; }

    public string NormalizedUsername { get; set; }

    [LocalizeDisplay(nameof(Resources.Email))]
    public string Email { get; set; }

    public string NormalizedEmail { get; set; }

    [LocalizeDisplay(nameof(Resources.PhoneNumber))]
    public string PhoneNumber { get; set; }

    public string PasswordHash { get; set; }

    [LocalizeDisplay(nameof(Resources.IsActive))]
    public bool IsActive { get; set; } = true;

    public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();

    public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}