using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class UserAchievement : BaseEntityUser<AppUser, AppRole>
{
    public Guid AchievementId { get; set; }
    [Display(Name = nameof(Achievement), ResourceType = typeof(App.Resources.Domain.UserAchievement))]
    public Achievement? Achievement { get; set; }
}