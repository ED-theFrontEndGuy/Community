using System.ComponentModel.DataAnnotations;
using App.Resources.Domain;
using Base.Interfaces;

namespace App.DAL.DTO;

public class RoomDto : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(128)]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Room))]
    public string Name { get; set; } = default!;
    
    [MaxLength(256)]
    public string Description { get; set; } = default!;

    [Display(Name = nameof(StudySessions), Prompt = nameof(StudySessions), ResourceType = typeof(App.Resources.Domain.Room))]
    public ICollection<StudySession>? StudySessions { get; set; }
}