using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.DTO.v1;

public class StudySession : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(256)]
    public string Description { get; set; } = default!;
    
    public bool Active { get; set; } = true;
    
    public Guid AssignmentId { get; set; }
    
    public string AssignmentName { get; set; } = default!;

    public Guid RoomId { get; set; }
    
    public string RoomName { get; set; } = default!;
}