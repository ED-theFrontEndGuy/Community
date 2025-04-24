using App.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class AttachmentCreateEditViewModel
{
    public Attachment Attachment { get; set; } = default!;
    
    [ValidateNever]
    public SelectList AssignmentSelectList { get; set; } = default!;
}