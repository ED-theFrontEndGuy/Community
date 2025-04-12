using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Achievement : BaseEntityUser<AppUser, AppRole>
{
    [MaxLength(128)]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Achievement))]
    public string Name { get; set; } = default!;

    [Display(Name = nameof(UserAchievements), Prompt = nameof(UserAchievements), ResourceType = typeof(App.Resources.Domain.Achievement))]
    public ICollection<UserAchievement>? UserAchievements { get; set; }
}