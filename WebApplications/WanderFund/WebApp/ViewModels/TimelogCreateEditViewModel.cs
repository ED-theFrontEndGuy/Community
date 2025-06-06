using App.BLL.DTO;
using App.DAL.DTO;
using App.Domain;
using Base.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class TimelogCreateEditViewModel //: BaseEntity
{
    public TimelogBLLDto Timelog { get; set; } = default!;

    [ValidateNever]
    public SelectList DeclarationSelectList { get; set; } = default!;
    
    [ValidateNever]
    public SelectList AssignmentSelectList { get; set; } = default!;
}