using App.BLL.DTO;
using App.DAL.DTO;
using App.Domain;
using Base.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class StudyGroupCreateEditViewModel : BaseEntity
{
    public StudyGroupBLLDto StudyGroup { get; set; } = default!;
    
    [ValidateNever]
    public SelectList StudySessionSelectList { get; set; } = default!;
}