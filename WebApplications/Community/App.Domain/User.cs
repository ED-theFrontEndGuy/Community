using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class User : BaseEntity
{
    [MaxLength(64)]
    public string UserName { get; set; } = default!;
    
    [MaxLength(128)]
    public string Email { get; set; } = default!;

    public ICollection<UserAchievement>? UserAchievements { get; set; }
    public ICollection<Dashboard>? Dashboards { get; set; }
    public ICollection<Declaration>? Declarations { get; set; }
    public ICollection<StudyGroup>? Groups { get; set; }
    public ICollection<Message>? Messages { get; set; }
} 