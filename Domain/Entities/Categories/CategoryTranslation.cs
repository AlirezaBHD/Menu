using System.ComponentModel.DataAnnotations;
using Domain.Common.Attributes;
using Domain.Localization;

namespace Domain.Entities.Categories;

public class CategoryTranslation : BaseTranslationEntity<Category>
{
    [MaxLength(50)]
    [LocalizeDisplay(nameof(Resources.Title))]
    public required string Title { get; set; }
}