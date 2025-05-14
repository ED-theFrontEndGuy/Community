using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;
using Base.Interfaces;

namespace App.DAL.DTO;

public class StudyGroupDto : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(128)]
    public string? Name { get; set; } = default!;
    
    public Guid StudySessionId { get; set; }
    [Display(Name = nameof(StudySession), Prompt = nameof(StudySession), ResourceType = typeof(App.Resources.Domain.StudyGroup))]
    public StudySessionDto? StudySession { get; set; }
    
    public Guid StudyGroupUserId { get; set; }
    public ICollection<StudyGroupUserDto>? StudyGroupUsers { get; set; }
}