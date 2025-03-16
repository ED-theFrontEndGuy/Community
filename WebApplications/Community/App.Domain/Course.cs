using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Course : BaseEntity
{
    [MaxLength(128)]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Course))]
    public string Name { get; set; } = default!;

    [Display(Name = nameof(Declarations), Prompt = nameof(Declarations), ResourceType = typeof(App.Resources.Domain.Course))]
    public ICollection<Declaration>? Declarations { get; set; }
}