using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.BLL.DTO;

public class AttachmentBLLDto : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(256)]
    [Display(Name = nameof(Link), Prompt = nameof(Link), ResourceType = typeof(App.Resources.Domain.Attachment))]
    public string Link { get; set; } = default!;
    
    [MaxLength(512)]
    public string Description { get; set; } = default!;

    public Guid AssignmentId { get; set; }
    [Display(Name = nameof(Assignment), Prompt = nameof(Assignment), ResourceType = typeof(App.Resources.Domain.Attachment))]
    public AssignmentBLLDto? Assignment { get; set; }
}