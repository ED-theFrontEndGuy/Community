using System.ComponentModel.DataAnnotations;
using App.BLL.DTO;
using Base.Interfaces;

namespace App.DTO.v1;

public class Attachment : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(256)]
    public string Link { get; set; } = default!;
    
    [MaxLength(512)]
    public string Description { get; set; } = default!;

    public Guid AssignmentId { get; set; }
    
    public string AssignmentName { get; set; } = default!;
}