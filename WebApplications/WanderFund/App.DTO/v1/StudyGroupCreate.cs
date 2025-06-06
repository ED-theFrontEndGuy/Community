using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1;

public class StudyGroupCreate
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    public Guid StudySessionId { get; set; }
}