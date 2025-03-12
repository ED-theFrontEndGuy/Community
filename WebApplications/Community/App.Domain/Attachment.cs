using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Attachment : BaseEntity
{
    [MaxLength(256)] public string Link { get; set; } = default!;

    public Guid AssignmentId { get; set; }
    public Assignment? Assignment { get; set; }
}