using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Room : BaseEntity
{
    [MaxLength(128)] public string Name { get; set; } = default!;

    public ICollection<StudySession>? StudySessions { get; set; }
}