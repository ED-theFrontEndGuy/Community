using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1;

public class StudySessionCreate
{
    [MaxLength(256)]
    public string Description { get; set; } = default!;
    
    public bool Active { get; set; } = true;
    
    public Guid AssignmentId { get; set; }

    public Guid RoomId { get; set; }
}