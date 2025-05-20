using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class StudySession : BaseEntity
{
    [MaxLength(256)]
    public string Description { get; set; } = default!;

    public bool Active { get; set; } = true;
    
    public Guid AssignmentId { get; set; }
    [Display(Name = nameof(Assignment), Prompt = nameof(Assignment), ResourceType = typeof(App.Resources.Domain.StudySession))]
    public Assignment? Assignment { get; set; }

    public Guid RoomId { get; set; }
    [Display(Name = nameof(Room), Prompt = nameof(Room), ResourceType = typeof(App.Resources.Domain.StudySession))]
    public Room? Room { get; set; }
    
    public StudyGroup? StudyGroup { get; set; }
}