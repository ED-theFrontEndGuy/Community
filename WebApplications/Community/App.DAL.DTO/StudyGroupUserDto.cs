using Base.Interfaces;

namespace App.DAL.DTO;

public class StudyGroupUserDto : IDomainId
{
    public Guid Id { get; set; }
    
    public Boolean isOwner { get; set; } = true;
    
    public Guid StudyGroupId { get; set; }
    public StudyGroupDto? StudyGroup { get; set; }
}