using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Message : BaseEntityUser<AppUser, AppRole>
{
    [MaxLength(256)]
    [Display(Name = nameof(UserMessage), Prompt = nameof(UserMessage), ResourceType = typeof(App.Resources.Domain.Message))]
    public string UserMessage { get; set; } = default!;
    
    public Guid ConversationId { get; set; }
    [Display(Name = nameof(Conversation), Prompt = nameof(Conversation), ResourceType = typeof(App.Resources.Domain.Message))]
    public Conversation? Conversation { get; set; }
}