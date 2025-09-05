using System.ComponentModel.DataAnnotations;
using Domain.Common.Attributes;
using Domain.Localization;

namespace Domain.Entities;

public class Language
{
    [Key]
    [MaxLength(10)]
    [LocalizeDisplay(nameof(Resources.Code))]
    public string Code { get; set; } = default!;

    [MaxLength(50)]
    [LocalizeDisplay(nameof(Resources.DisplayName))]
    public string DisplayName { get; set; } = default!;

    public bool IsRtl { get; set; }
}
