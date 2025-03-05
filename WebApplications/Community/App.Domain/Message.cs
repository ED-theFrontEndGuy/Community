using Base.Domain;

namespace App.Domain;

public class Message : BaseEntity
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    public Guid ConversationId { get; set; }
    public Conversation? Conversation { get; set; }
}