namespace App.DTO.v1;

public class TimelogCreate
{
    public DateTime StartTime { get; set; }
    
    public DateTime? EndTime { get; set; }
    
    public TimeSpan? Duration { get; set; }

    public Guid DeclarationId { get; set; }

    public Guid AssignmentId { get; set; }
}