using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1;

public class CourseCreate
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
}