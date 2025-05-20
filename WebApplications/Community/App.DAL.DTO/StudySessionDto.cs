using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.DAL.DTO;

public class StudySessionDto : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(256)]
    public string Description { get; set; } = default!;
    
    public bool Active { get; set; } = true;
    
    public Guid AssignmentId { get; set; }
    [Display(Name = nameof(Assignment), Prompt = nameof(Assignment), ResourceType = typeof(App.Resources.Domain.StudySession))]
    public AssignmentDto? Assignment { get; set; }

    public Guid RoomId { get; set; }
    [Display(Name = nameof(Room), Prompt = nameof(Room), ResourceType = typeof(App.Resources.Domain.StudySession))]
    public RoomDto? Room { get; set; }
    
    public StudyGroupDto? StudyGroup { get; set; }
}