using Base.Interfaces;

namespace App.DTO.v1;

public class Declaration : IDomainId
{
    public Guid Id { get; set; }
    
    public Boolean Active { get; set; }
    
    public Guid CourseId { get; set; }
    
    public string CourseName { get; set; } = default!;
}