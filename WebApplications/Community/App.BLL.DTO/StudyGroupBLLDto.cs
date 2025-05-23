using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.BLL.DTO;

public class StudyGroupBLLDto : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(128)]
    public string? Name { get; set; } = default!;
    
    public Guid StudySessionId { get; set; }
    [Display(Name = nameof(StudySession), Prompt = nameof(StudySession), ResourceType = typeof(App.Resources.Domain.StudyGroup))]
    public StudySessionBLLDto? StudySession { get; set; }
    
    public ICollection<StudyGroupUserBLLDto>? StudyGroupUsers { get; set; }
}