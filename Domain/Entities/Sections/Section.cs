using System.ComponentModel;
using Domain.Entities.Categories;
using Domain.Entities.MenuItems;
using Domain.Interfaces;

namespace Domain.Entities.Sections;

[DisplayName("بخش")]
public class Section: BaseEntity, ITranslation<SectionTranslation>
{
    public ActivityPeriod ActivityPeriod { get; set; } = new();
    public int Order { get; set; }
    public int? CategoryId { get; set; }

    public Category? Category { get; set; }
    public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    
    public ICollection<SectionTranslation> Translations { get; set; } = new List<SectionTranslation>();
}
