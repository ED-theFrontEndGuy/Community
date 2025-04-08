using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Dashboard : BaseEntity
{
    [MaxLength(128)]
    [Display(Name = nameof(ConfigJson), Prompt = nameof(ConfigJson), ResourceType = typeof(App.Resources.Domain.Dashboard))]
    public string ConfigJson { get; set; } = default!;

    public Guid UserId { get; set; }
    [Display(Name = nameof(User), Prompt = nameof(User), ResourceType = typeof(App.Resources.Domain.Dashboard))]
    public AppUser? User { get; set; }
}