using System.ComponentModel.DataAnnotations;
using Domain.Common.Attributes;
using Domain.Localization;

namespace Domain.Entities.MenuItems;

public sealed class MenuItemTranslation : BaseTranslationEntity<MenuItem>
{
    [MaxLength(50)]
    [LocalizeDisplay(nameof(Resources.Title))]
    public required string Title { get; set; }
    
    [MaxLength(300)]
    [LocalizeDisplay(nameof(Resources.Description))]
    public string? Description { get; set; }
}
