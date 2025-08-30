using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.MenuItems;

public sealed class MenuItemTranslation : BaseEntity
{
    [MaxLength(50)]
    public required string Title { get; set; }
    
    [MaxLength(300)]
    public string? Description { get; set; }

    public int MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; } = default!;
    
    [MaxLength(10)]
    public string LanguageCode { get; set; } = default!;
    public Language Language { get; set; } = default!;
}
