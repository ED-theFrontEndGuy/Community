using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Dashboard : BaseEntityUser<AppUser, AppRole>
{
    [MaxLength(128)]
    [Display(Name = nameof(ConfigJson), Prompt = nameof(ConfigJson), ResourceType = typeof(App.Resources.Domain.Dashboard))]
    public string ConfigJson { get; set; } = default!;
}