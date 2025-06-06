using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1;

public class AttachmentCreate
{
    [MaxLength(256)]
    public string Link { get; set; } = default!;
    
    [MaxLength(512)]
    public string Description { get; set; } = default!;

    public Guid AssignmentId { get; set; }
}