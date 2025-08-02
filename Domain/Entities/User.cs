namespace Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }

    public string NormalizedUsername { get; set; }

    public string Email { get; set; }

    public string NormalizedEmail { get; set; }

    public string PhoneNumber { get; set; }

    public string PasswordHash { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<UserRole> Roles { get; set; } = new List<UserRole>();

    public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}