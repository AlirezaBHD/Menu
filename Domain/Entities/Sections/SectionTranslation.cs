using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Sections;

public class SectionTranslation : BaseTranslationEntity<Section>
{
    [MaxLength(50)]
    [Display(Name = "عنوان")]
    public required string Title { get; set; }
}