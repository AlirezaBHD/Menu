using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Language
{
    [Key]
    [MaxLength(10)]
    public string Code { get; set; } = default!;

    [MaxLength(50)]
    public string DisplayName { get; set; } = default!;

    public bool IsRtl { get; set; }
}
