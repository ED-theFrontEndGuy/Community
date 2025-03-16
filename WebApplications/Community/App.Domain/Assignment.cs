using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Assignment : BaseEntity
{
    [MaxLength(128)] public string Name { get; set; } = default!;

    public Guid DeclarationId { get; set; }
    public Declaration? Declaration { get; set; }
    
    public ICollection<Timelog>? Timelogs { get; set; }
    public ICollection<Attachment>? Attachments { get; set; }
    public ICollection<StudySession>? StudySessions { get; set; }
}