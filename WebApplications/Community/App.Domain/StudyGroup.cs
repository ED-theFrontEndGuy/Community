using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class StudyGroup : BaseEntityUser<AppUser, AppRole>
{
    public Guid StudySessionId { get; set; }
    [Display(Name = nameof(StudySession), Prompt = nameof(StudySession), ResourceType = typeof(App.Resources.Domain.StudyGroup))]
    public StudySession? StudySession { get; set; }

    [Display(Name = nameof(Conversations), Prompt = nameof(Conversations), ResourceType = typeof(App.Resources.Domain.StudyGroup))]
    public ICollection<Conversation>? Conversations { get; set; }
}