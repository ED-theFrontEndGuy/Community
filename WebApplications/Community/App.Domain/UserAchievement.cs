using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class UserAchievement : BaseEntity
{
    public Guid AchievementId { get; set; }
    [Display(Name = nameof(Achievement), ResourceType = typeof(App.Resources.Domain.UserAchievement))]
    public Achievement? Achievement { get; set; }

    public Guid UserId { get; set; }
    [Display(Name = nameof(User), ResourceType = typeof(App.Resources.Domain.UserAchievement))]
    public AppUser? User { get; set; }
}