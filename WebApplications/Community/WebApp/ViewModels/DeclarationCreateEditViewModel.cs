using App.DAL.DTO;
using App.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class DeclarationCreateEditViewModel
{
    public DeclarationDto Declaration { get; set; } = default!;

    [ValidateNever]
    public SelectList CourseSelectList { get; set; } = default!;
}