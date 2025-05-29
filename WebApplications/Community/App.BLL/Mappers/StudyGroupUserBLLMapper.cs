using App.BLL.DTO;
using App.DAL.DTO;
using Base.Interfaces;

namespace App.BLL.Mappers;

public class StudyGroupUserBLLMapper : IMapper<StudyGroupUserBLLDto, StudyGroupUserDto>
{
    public StudyGroupUserBLLDto? Map(StudyGroupUserDto? entity)
    {
        if (entity == null) return null;

        var res = new StudyGroupUserBLLDto()
        {
            Id = entity.Id,
            isOwner = entity.isOwner,
            StudyGroupId = entity.StudyGroupId,
            StudyGroup = entity.StudyGroup == null
                ? null
                : new StudyGroupBLLDto()
                {
                    Id = entity.StudyGroup.Id,
                    Name = entity.StudyGroup.Name,
                    StudySessionId = entity.StudyGroup.StudySessionId,
                },
        };
        
        return res;
    }

    public StudyGroupUserDto? Map(StudyGroupUserBLLDto? entity)
    {
        if (entity == null) return null;
        
        var res = new StudyGroupUserDto()
        {
            Id = entity.Id,
            isOwner = entity.isOwner,
            StudyGroupId = entity.StudyGroupId,
        };

        return res;
    }
}