using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class BaseTranslationEntity<T> : BaseEntity
{
    public int CoreId { get; set; }

    [ForeignKey(nameof(CoreId))]
    public T Core { get; set; } = default!;

    [MaxLength(10)] public string LanguageCode { get; set; } = default!;
    public Language Language { get; set; } = default!;
}