using App.BLL.DTO;
using App.DAL.DTO;
using Base.Interfaces;

namespace App.BLL.Mappers;

public class StudyGroupBLLMapper : IMapper<StudyGroupBLLDto, StudyGroupDto>
{
    public StudyGroupBLLDto? Map(StudyGroupDto? entity)
    {
        if (entity == null) return null;

        var res = new StudyGroupBLLDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            StudySessionId = entity.StudySessionId,
            StudySession = entity.StudySession == null
                ? null
                : new StudySessionBLLDto()
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

    public StudyGroupDto? Map(StudyGroupBLLDto? entity)
    {
        if (entity == null) return null;
        
        var res = new StudyGroupDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            StudySessionId = entity.StudySessionId,
        };

        return res;
    }
}