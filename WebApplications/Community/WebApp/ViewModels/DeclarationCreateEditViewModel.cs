using App.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class DeclarationCreateEditViewModel
{
    public Declaration Declaration { get; set; } = default!;

    [ValidateNever]
    public SelectList CourseSelectList { get; set; } = default!;
}