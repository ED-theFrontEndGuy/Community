using App.Domain;
using Base.Domain;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels;

public class UserAchievementsCreateEditViewModel : BaseEntity
{
    public UserAchievement UserAchievement { get; set; } = default!;

    [ValidateNever]
    public SelectList AchievementSelectList { get; set; } = default!;
}