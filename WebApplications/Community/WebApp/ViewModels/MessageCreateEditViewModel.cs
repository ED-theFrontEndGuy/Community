using Base.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Message = App.Domain.Message;

namespace WebApp.ViewModels;

public class MessageCreateEditViewModel : BaseEntity
{
    public Message Message { get; set; } = default!;
    
    [ValidateNever]
    public SelectList ConversationSelectList { get; set; } = default!;
}