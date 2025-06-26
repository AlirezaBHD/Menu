using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}
