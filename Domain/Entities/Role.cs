using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; }

    public string NormalizedName { get; set; }

    public ICollection<UserRole> Users { get; set; } = new List<UserRole>();
}