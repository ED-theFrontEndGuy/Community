using Base.Interfaces;

namespace App.DTO.v1;

public class Timelog : IDomainId
{
    public Guid Id { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime? EndTime { get; set; }
    
    public TimeSpan? Duration { get; set; }

    public Guid DeclarationId { get; set; }
    public string CourseName { get; set; } = default!;

    public Guid AssignmentId { get; set; }
    public string AssignmentName { get; set; } = default!;
}