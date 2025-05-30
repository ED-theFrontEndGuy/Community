using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1;

public class AssignmentCreate
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;

    public Guid DeclarationId { get; set; }
}