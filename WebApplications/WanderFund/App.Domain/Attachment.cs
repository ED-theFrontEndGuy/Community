using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Attachment : BaseEntity
{
    [MaxLength(256)]
    [Display(Name = nameof(Link), Prompt = nameof(Link), ResourceType = typeof(App.Resources.Domain.Attachment))]
    public string Link { get; set; } = default!;
    
    [MaxLength(512)]
    public string Description { get; set; } = default!;

    public Guid AssignmentId { get; set; }
    [Display(Name = nameof(Assignment), Prompt = nameof(Assignment), ResourceType = typeof(App.Resources.Domain.Attachment))]
    public Assignment? Assignment { get; set; }
}