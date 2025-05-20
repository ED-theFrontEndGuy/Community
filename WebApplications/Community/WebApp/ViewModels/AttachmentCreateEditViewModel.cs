using App.DAL.DTO;
using App.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class AttachmentCreateEditViewModel
{
    public AttachmentDto Attachment { get; set; } = default!;
    
    [ValidateNever]
    public SelectList AssignmentSelectList { get; set; } = default!;
}