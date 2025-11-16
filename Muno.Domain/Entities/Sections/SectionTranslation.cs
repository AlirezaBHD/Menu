using System.ComponentModel.DataAnnotations;
using Muno.Domain.Common.Attributes;
using Muno.Domain.Localization;

namespace Muno.Domain.Entities.Sections;

public class SectionTranslation : BaseTranslationEntity<Section>
{
    [MaxLength(50)]
    [LocalizeDisplay(nameof(Resources.Title))]
    public required string Title { get; set; }
}