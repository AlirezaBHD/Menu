using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Categories;

public class CategoryTranslation : BaseTranslationEntity<Category>
{
    [MaxLength(50)]
    [Display(Name = "عنوان")]
    public required string Title { get; set; }
}