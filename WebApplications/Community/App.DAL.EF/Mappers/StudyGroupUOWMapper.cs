using App.DAL.DTO;
using App.Domain;
using Base.Interfaces;

namespace App.DAL.EF.Mappers;

public class StudyGroupUOWMapper : IMapper<StudyGroupDto, StudyGroup>
{
    public StudyGroupDto? Map(StudyGroup? entity)
    {
        if (entity == null) return null;

        var res = new StudyGroupDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            StudySessionId = entity.StudySessionId,
            StudySession = entity.StudySession == null
                ? null
                : new StudySessionDto()
                {
                    Id = entity.StudySession.Id,
                    Description = entity.StudySession.Description,
                    Active = entity.StudySession.Active,
                    AssignmentId = entity.StudySession.AssignmentId,
                    RoomId = entity.StudySession.RoomId,
                }
        };

        return res;
    }

    public StudyGroup? Map(StudyGroupDto? entity)
    {
        if (entity == null) return null;
        
        var res = new StudyGroup()
        {
            Id = entity.Id,
            Name = entity.Name,
            StudySessionId = entity.StudySessionId,
        };

        return res;
    }
}