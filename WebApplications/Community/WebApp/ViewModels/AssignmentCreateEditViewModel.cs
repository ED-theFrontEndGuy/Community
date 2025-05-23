using App.BLL.DTO;
using App.DAL.DTO;
using App.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class AssignmentCreateEditViewModel
{
    public AssignmentBLLDto Assignment { get; set; } = default!;

    [ValidateNever]
    public SelectList DeclarationSelectList { get; set; } = default!;
}