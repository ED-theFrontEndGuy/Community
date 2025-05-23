using Base.Interfaces;

namespace App.BLL.DTO;

public class StudyGroupUserBLLDto : IDomainId
{
    public Guid Id { get; set; }
    
    public Boolean isOwner { get; set; } = true;
    
    public Guid StudyGroupId { get; set; }
    public StudyGroupBLLDto? StudyGroup { get; set; }
}