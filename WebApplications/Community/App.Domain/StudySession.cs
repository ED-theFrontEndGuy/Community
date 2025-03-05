using Base.Domain;

namespace App.Domain;

public class StudySession : BaseEntity
{
    public Guid AssignmentId { get; set; }
    public Assignment? Assignment { get; set; }

    public Guid RoomId { get; set; }
    public Room? Room { get; set; }

    public Guid StudyGroupId { get; set; }
    public StudyGroup? StudyGroup { get; set; }
}