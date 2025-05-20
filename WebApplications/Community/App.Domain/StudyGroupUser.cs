using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class StudyGroupUser : BaseEntityUser<AppUser>
{
    public Boolean isOwner { get; set; } = true;
    
    public Guid StudyGroupId { get; set; }
    public StudyGroup? StudyGroup { get; set; }
}