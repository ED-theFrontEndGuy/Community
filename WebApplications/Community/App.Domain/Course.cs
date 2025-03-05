using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Course : BaseEntity
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;

    public ICollection<Declaration>? Declarations { get; set; }
    public ICollection<Assignment>? Tasks { get; set; }
}