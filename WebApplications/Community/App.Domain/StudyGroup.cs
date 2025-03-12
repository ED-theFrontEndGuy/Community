using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class StudyGroup : BaseEntity
{
    public Guid StudySessionId { get; set; }
    public StudySession? StudySession { get; set; }

    public ICollection<Conversation>? Conversations { get; set; }
}