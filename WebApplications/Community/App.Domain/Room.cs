using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Room : BaseEntity
{
    [MaxLength(128)]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Room))]
    public string Name { get; set; } = default!;

    [Display(Name = nameof(StudySessions), Prompt = nameof(StudySessions), ResourceType = typeof(App.Resources.Domain.Room))]
    public ICollection<StudySession>? StudySessions { get; set; }
}