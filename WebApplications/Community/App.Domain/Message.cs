using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Message : BaseEntity
{
    [MaxLength(256)]
    [Display(Name = nameof(UserMessage), Prompt = nameof(UserMessage), ResourceType = typeof(App.Resources.Domain.Message))]
    public string UserMessage { get; set; } = default!;
    
    public Guid UserId { get; set; }
    [Display(Name = nameof(User), Prompt = nameof(User), ResourceType = typeof(App.Resources.Domain.Message))]
    public AppUser? User { get; set; }
    
    public Guid ConversationId { get; set; }
    [Display(Name = nameof(Conversation), Prompt = nameof(Conversation), ResourceType = typeof(App.Resources.Domain.Message))]
    public Conversation? Conversation { get; set; }
}