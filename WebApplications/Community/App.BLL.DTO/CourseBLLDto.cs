using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.BLL.DTO;

public class CourseBLLDto : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(128)]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Course))]
    public string Name { get; set; } = default!;

    [Display(Name = nameof(Declarations), Prompt = nameof(Declarations), ResourceType = typeof(App.Resources.Domain.Course))]
    public ICollection<DeclarationBLLDto>? Declarations { get; set; }
}