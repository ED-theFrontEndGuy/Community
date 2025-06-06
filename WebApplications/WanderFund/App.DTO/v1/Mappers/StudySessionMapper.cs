using App.BLL.DTO;
using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class StudySessionMapper : IMapper<StudySession, StudySessionBLLDto>
{
    public StudySession? Map(StudySessionBLLDto? entity)
    {
        if (entity == null) return null;

        var res = new StudySession()
        {
            Id = entity.Id,
            Description = entity.Description,
            Active = entity.Active,
            AssignmentId = entity.AssignmentId,
            AssignmentName = (entity.Assignment == null
                ? null
                : entity.Assignment.Name)!,
            RoomId = entity.RoomId,
            RoomName = (entity.Room == null
                ? null
                : entity.Room.Name)!,
        };

        return res;
    }

    public StudySessionBLLDto? Map(StudySession? entity)
    {
        if (entity == null) return null;

        var res = new StudySessionBLLDto()
        {
            Id = entity.Id,
            Description = entity.Description,
            Active = entity.Active,
            AssignmentId = entity.AssignmentId,
            RoomId = entity.RoomId
        };
        
        return res;
    }
    
    public StudySessionBLLDto Map(StudySessionCreate entity)
    {
        var res = new StudySessionBLLDto()
        {
            Id = Guid.NewGuid(),
            Description = entity.Description,
            Active = entity.Active,
            AssignmentId = entity.AssignmentId,
            RoomId = entity.RoomId
        };
        
        return res;
    }
}