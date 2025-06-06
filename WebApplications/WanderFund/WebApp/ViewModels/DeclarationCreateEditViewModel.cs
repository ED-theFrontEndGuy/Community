using App.BLL.DTO;
using App.DAL.DTO;
using App.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class DeclarationCreateEditViewModel
{
    public DeclarationBLLDto Declaration { get; set; } = default!;

    [ValidateNever]
    public SelectList CourseSelectList { get; set; } = default!;
}