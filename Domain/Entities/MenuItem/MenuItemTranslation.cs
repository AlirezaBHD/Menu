using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.MenuItem;

public sealed class MenuItemTranslation : BaseEntity
{
    [MaxLength(50)]
    public required string Title { get; set; }
    
    [MaxLength(300)]
    public string? Description { get; set; }

    public int MenuItemId { get; set; }
    public MenuItem MenuItem { get; set; } = default!;
    
    public int LanguageId { get; set; }
    public Language Language { get; set; } = default!;
}
