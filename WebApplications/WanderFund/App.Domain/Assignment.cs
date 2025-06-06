using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Assignment : BaseEntity
{
    [MaxLength(128)]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Assignment))]
    public string Name { get; set; } = default!;

    public Guid DeclarationId { get; set; }
    [Display(Name = nameof(Declaration), Prompt = nameof(Declaration), ResourceType = typeof(App.Resources.Domain.Assignment))]
    public Declaration? Declaration { get; set; }
    
    [Display(Name = nameof(Timelogs), Prompt = nameof(Timelogs), ResourceType = typeof(App.Resources.Domain.Assignment))]
    public ICollection<Timelog>? Timelogs { get; set; }
    
    [Display(Name = nameof(Attachments), Prompt = nameof(Attachments), ResourceType = typeof(App.Resources.Domain.Assignment))]
    public ICollection<Attachment>? Attachments { get; set; }
    
    [Display(Name = nameof(StudySessions), Prompt = nameof(StudySessions), ResourceType = typeof(App.Resources.Domain.Assignment))]
    public ICollection<StudySession>? StudySessions { get; set; }
}