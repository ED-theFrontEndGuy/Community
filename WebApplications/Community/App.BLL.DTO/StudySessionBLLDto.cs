using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.BLL.DTO;

public class StudySessionBLLDto : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(256)]
    public string Description { get; set; } = default!;
    
    public bool Active { get; set; } = true;
    
    public Guid AssignmentId { get; set; }
    [Display(Name = nameof(Assignment), Prompt = nameof(Assignment), ResourceType = typeof(App.Resources.Domain.StudySession))]
    public AssignmentBLLDto? Assignment { get; set; }

    public Guid RoomId { get; set; }
    [Display(Name = nameof(Room), Prompt = nameof(Room), ResourceType = typeof(App.Resources.Domain.StudySession))]
    public RoomBLLDto? Room { get; set; }
    
    public StudyGroupBLLDto? StudyGroup { get; set; }
}