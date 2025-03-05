using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class StudyGroup : BaseEntity
{
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public Guid ConversationId { get; set; }
    public Conversation? Conversation { get; set; }

    public ICollection<StudySession>? StudySessions { get; set; }
}