using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Timelog : BaseEntity
{
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }

    public Guid DeclarationId { get; set; }
    public Declaration? Declaration { get; set; }

    public Guid AssignmentId { get; set; }
    public Assignment? Assignment { get; set; }
}