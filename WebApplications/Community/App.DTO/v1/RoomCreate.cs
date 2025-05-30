using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1;

public class RoomCreate
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    [MaxLength(256)]
    public string Description { get; set; } = default!;
}