using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Conversation : BaseEntity
{
    [MaxLength(128)]
    public string Name { get; set; }

    public ICollection<StudyGroup>? Groups { get; set; }
    public ICollection<Message>? Messages { get; set; }
}