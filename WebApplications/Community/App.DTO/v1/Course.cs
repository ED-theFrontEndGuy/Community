using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.DTO.v1;

public class Course : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(128)]
    public string Name { get; set; } = default!;
}