using App.DAL.DTO;
using App.Domain;
using Base.Interfaces;

namespace App.DAL.EF.Mappers;

public class StudyGroupUserUOWMapper : IMapper<StudyGroupUserDto, StudyGroupUser>
{
    public StudyGroupUserDto? Map(StudyGroupUser? entity)
    {
        if (entity == null) return null;

        var res = new StudyGroupUserDto()
        {
            Id = entity.Id,
            isOwner = entity.isOwner,
            StudyGroupId = entity.StudyGroupId,
            StudyGroup = entity.StudyGroup == null
                ? null
                : new StudyGroupDto()
                {
                    Id = entity.StudyGroup.Id,
                    Name = entity.StudyGroup.Name,
                    StudySessionId = entity.StudyGroup.StudySessionId,
                },
        };
        
        return res;
    }

    public StudyGroupUser? Map(StudyGroupUserDto? entity)
    {
        if (entity == null) return null;
        
        var res = new StudyGroupUser()
        {
            Id = entity.Id,
            isOwner = entity.isOwner,
            StudyGroupId = entity.StudyGroupId,
        };

        return res;
    }
}