using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Sections;

public class SectionTranslation : BaseEntity
{
    [MaxLength(50)]
    [Display(Name = "عنوان")]
    public required string Title { get; set; }
    
    public int SectionId { get; set; }
    public Section Section { get; set; } = default!;
    
    [MaxLength(10)]
    public string LanguageCode { get; set; } = default!;
    public Language Language { get; set; } = default!;
}