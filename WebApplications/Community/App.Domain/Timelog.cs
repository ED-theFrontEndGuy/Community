using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Timelog : BaseEntity
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public Guid DeclarationId { get; set; }
    public Declaration? Declaration { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }

    public Guid AssignmentId { get; set; }
    public Assignment? Assignment { get; set; }
}