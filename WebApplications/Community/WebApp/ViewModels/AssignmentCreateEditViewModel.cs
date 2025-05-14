using App.DAL.DTO;
using App.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class AssignmentCreateEditViewModel
{
    public AssignmentDto Assignment { get; set; } = default!;

    [ValidateNever]
    public SelectList DeclarationSelectList { get; set; } = default!;
}