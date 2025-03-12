using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Message : BaseEntity
{
    [MaxLength(256)]
    public string UserMessage { get; set; } = default!;
    
    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    public Guid ConversationId { get; set; }
    public Conversation? Conversation { get; set; }
}