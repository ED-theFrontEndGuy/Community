using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;
using Base.Interfaces;

namespace App.DAL.DTO;

public class AttachmentDto : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(256)]
    [Display(Name = nameof(Link), Prompt = nameof(Link), ResourceType = typeof(App.Resources.Domain.Attachment))]
    public string Link { get; set; } = default!;

    public Guid AssignmentId { get; set; }
    [Display(Name = nameof(Assignment), Prompt = nameof(Assignment), ResourceType = typeof(App.Resources.Domain.Attachment))]
    public AssignmentDto? Assignment { get; set; }
}