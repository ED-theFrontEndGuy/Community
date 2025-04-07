using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace App.Domain;

public class User : IdentityUser<Guid>
{
    [MaxLength(64)]
    [Display(Name = nameof(UserName), ResourceType = typeof(App.Resources.Domain.User))]
    public string UserName { get; set; } = default!;
    
    [MaxLength(128)]
    [Display(Name = nameof(Email), ResourceType = typeof(App.Resources.Domain.User))]
    public string Email { get; set; } = default!;

    [Display(Name = nameof(UserAchievements), ResourceType = typeof(App.Resources.Domain.User))]
    public ICollection<UserAchievement>? UserAchievements { get; set; }
    [Display(Name = nameof(Dashboards), ResourceType = typeof(App.Resources.Domain.User))]
    public ICollection<Dashboard>? Dashboards { get; set; }
    [Display(Name = nameof(Declarations), ResourceType = typeof(App.Resources.Domain.User))]
    public ICollection<Declaration>? Declarations { get; set; }
    [Display(Name = nameof(Messages), ResourceType = typeof(App.Resources.Domain.User))]
    public ICollection<Message>? Messages { get; set; }
}