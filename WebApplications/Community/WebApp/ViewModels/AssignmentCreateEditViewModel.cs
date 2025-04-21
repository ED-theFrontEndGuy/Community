using App.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class AssignmentCreateEditViewModel
{
    public Assignment Assignment { get; set; } = default!;

    [ValidateNever]
    public SelectList DeclarationSelectList { get; set; } = default!;
}