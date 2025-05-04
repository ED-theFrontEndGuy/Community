using System.ComponentModel.DataAnnotations;
using Base.Domain.Identity;

namespace App.Domain.Identity;

public class AppUser : BaseUser<AppUserRole>
{
    [MinLength(1)]
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;
    
    [MinLength(1)]
    [MaxLength(128)]
    public string LastName { get; set; } = default!;
    
    [Display(Name = nameof(UserAchievements), ResourceType = typeof(App.Resources.Domain.User))]
    public ICollection<UserAchievement>? UserAchievements { get; set; }
    
    [Display(Name = nameof(Dashboards), ResourceType = typeof(App.Resources.Domain.User))]
    public ICollection<Dashboard>? Dashboards { get; set; }
    
    [Display(Name = nameof(Declarations), ResourceType = typeof(App.Resources.Domain.User))]
    public ICollection<Declaration>? Declarations { get; set; }
    
    [Display(Name = nameof(Messages), ResourceType = typeof(App.Resources.Domain.User))]
    public ICollection<Message>? Messages { get; set; }

    public ICollection<AppRefreshToken>? AppRefreshTokens { get; set; }
}