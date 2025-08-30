using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Categories;

public class CategoryTranslation : BaseEntity
{
    [MaxLength(50)]
    [Display(Name = "عنوان")]
    public required string Title { get; set; }
    
    public int CategoryId { get; set; }
    public Category Category { get; set; } = default!;
    
    [MaxLength(10)]
    public string LanguageCode { get; set; } = default!;
    public Language Language { get; set; } = default!;
}