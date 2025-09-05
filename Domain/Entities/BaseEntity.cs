using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common.Attributes;
using Domain.Localization;

namespace Domain.Entities;

public abstract class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [LocalizeDisplay(nameof(Resources.Identity))]
    public int Id { get; set; }
    
    [LocalizeDisplay(nameof(Resources.CreateDate))]
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    
    [LocalizeDisplay(nameof(Resources.ModifyDate))]
    public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
}
