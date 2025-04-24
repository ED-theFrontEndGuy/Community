using App.Domain;
using Base.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class ConversationCreateEditViewModel : BaseEntity
{
    public Conversation Conversation { get; set; } = default!;

    [ValidateNever]
    public SelectList StudyGroupSelectList { get; set; } = default!;
}