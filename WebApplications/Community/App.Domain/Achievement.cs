using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Achievement : BaseEntity
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;

    public ICollection<UserAchievement>? UserAchievements { get; set; }
}