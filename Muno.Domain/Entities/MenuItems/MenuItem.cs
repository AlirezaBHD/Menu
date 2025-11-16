using System.ComponentModel;
using Muno.Domain.Interfaces;
using Muno.Domain.Common.Attributes;
using Muno.Domain.Entities.MenuItemVariants;
using Muno.Domain.Entities.Sections;
using Muno.Domain.Interfaces.Specifications;
using Muno.Domain.Localization;

namespace Muno.Domain.Entities.MenuItems;

[DisplayName("آیتم منو")]
public class MenuItem: BaseEntity, ITranslation<MenuItemTranslation>
{
    [LocalizeDisplay(nameof(Resources.ImagePath))]
    public string? ImagePath { get; set; }

    [LocalizeDisplay(nameof(Resources.IsAvailable))]
    public bool IsAvailable { get; set; } = true;
    
    public ICollection<MenuItemTranslation> Translations { get; set; } = new List<MenuItemTranslation>();
    public ActivityPeriod ActivityPeriod { get; set; } = new();
    
    public int Order { get; set; }

    public List<MenuItemVariant> Variants { get; set; } = new();
    public int? SectionId { get; set; }
    public Section? Section { get; set; }
}
