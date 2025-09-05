using Domain.Common.Attributes;
using Domain.Localization;

namespace Domain.Entities;

public class Role : BaseEntity
{
    [LocalizeDisplay(nameof(Resources.Name))]
    public string Name { get; set; }

    public string NormalizedName { get; set; }

    public ICollection<UserRole> Users { get; set; } = new List<UserRole>();
}