using App.DAL.DTO;
using App.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class StudySessionCreateEditViewModel
{
    public StudySessionDto StudySession { get; set; } = default!;
    
    [ValidateNever]
    public SelectList AssignmentSelectList { get; set; } = default!;
    
    [ValidateNever]
    public SelectList RoomSelectList { get; set; } = default!;
}