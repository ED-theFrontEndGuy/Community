using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class StudyGroup : BaseEntity
{
    [MaxLength(128)]
    public string? Name { get; set; } = default!;
    
    public Guid StudySessionId { get; set; }
    [Display(Name = nameof(StudySession), Prompt = nameof(StudySession), ResourceType = typeof(App.Resources.Domain.StudyGroup))]
    public StudySession? StudySession { get; set; }
    
    public Guid StudyGroupUserId { get; set; }
    public ICollection<StudyGroupUser>? StudyGroupUsers { get; set; }
}