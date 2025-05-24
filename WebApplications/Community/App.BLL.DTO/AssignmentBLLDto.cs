using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.BLL.DTO;

public class AssignmentBLLDto : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(128)]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Assignment))]
    public string Name { get; set; } = default!;

    public Guid DeclarationId { get; set; }
    [Display(Name = nameof(Declaration), Prompt = nameof(Declaration), ResourceType = typeof(App.Resources.Domain.Assignment))]
    public DeclarationBLLDto? Declaration { get; set; }
    
    [Display(Name = nameof(Timelogs), Prompt = nameof(Timelogs), ResourceType = typeof(App.Resources.Domain.Assignment))]
    public ICollection<TimelogBLLDto>? Timelogs { get; set; }
    
    [Display(Name = nameof(Attachments), Prompt = nameof(Attachments), ResourceType = typeof(App.Resources.Domain.Assignment))]
    public ICollection<AttachmentBLLDto>? Attachments { get; set; }
    
    [Display(Name = nameof(StudySessions), Prompt = nameof(StudySessions), ResourceType = typeof(App.Resources.Domain.Assignment))]
    public ICollection<StudySessionBLLDto>? StudySessions { get; set; }
}