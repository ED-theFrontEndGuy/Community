using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : IdentityUser<Guid>
{
    [Display(Name = nameof(UserAchievements), ResourceType = typeof(App.Resources.Domain.User))]
    public ICollection<UserAchievement>? UserAchievements { get; set; }
    
    [Display(Name = nameof(Dashboards), ResourceType = typeof(App.Resources.Domain.User))]
    public ICollection<Dashboard>? Dashboards { get; set; }
    
    [Display(Name = nameof(Declarations), ResourceType = typeof(App.Resources.Domain.User))]
    public ICollection<Declaration>? Declarations { get; set; }
    
    [Display(Name = nameof(Messages), ResourceType = typeof(App.Resources.Domain.User))]
    public ICollection<Message>? Messages { get; set; }
}