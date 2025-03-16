using Base.Domain;

namespace App.Domain;

public class Declaration : BaseEntity
{
    public Boolean Active { get; set; } = false;
    
    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }

    public ICollection<Timelog>? Timelogs { get; set; }
    public ICollection<Assignment>? Assignments { get; set; }
}