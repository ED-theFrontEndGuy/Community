using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Conversation : BaseEntity
{
    [MaxLength(128)]
    [Display(Name = nameof(Name), Prompt = nameof(Name), ResourceType = typeof(App.Resources.Domain.Conversation))]
    public string Name { get; set; } = default!;

    public Guid StudyGroupId { get; set; }
    [Display(Name = nameof(StudyGroup), Prompt = nameof(StudyGroup), ResourceType = typeof(App.Resources.Domain.Conversation))]
    public StudyGroup? StudyGroup { get; set; }
    
    [Display(Name = nameof(Messages), Prompt = nameof(Messages), ResourceType = typeof(App.Resources.Domain.Conversation))]
    public ICollection<Message>? Messages { get; set; }
}