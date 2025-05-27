using App.BLL.DTO;
using App.DAL.DTO;
using Base.Interfaces;

namespace App.BLL.Mappers;

public class StudySessionBLLMapper : IMapper<StudySessionBLLDto, StudySessionDto>
{
    public StudySessionBLLDto? Map(StudySessionDto? entity)
    {
        if (entity == null) return null;

        var res = new StudySessionBLLDto()
        {
            Id = entity.Id,
            Description = entity.Description,
            Active = entity.Active,
            AssignmentId = entity.AssignmentId,
            Assignment = entity.Assignment == null
                ? null
                : new AssignmentBLLDto()
                {
                    Id = entity.Assignment.Id,
                    Name = entity.Assignment.Name,
                },
            RoomId = entity.RoomId,
            Room = entity.Room == null
                ? null
                : new RoomBLLDto()
                {
                    Id = entity.Room.Id,
                    Name = entity.Room.Name,
                },
            StudyGroup = entity.StudyGroup == null
                ? null
                : new StudyGroupBLLDto()
                {
                    Id = entity.StudyGroup.Id,
                    Name = entity.StudyGroup.Name,
                }
        };

        return res;
    }

    public StudySessionDto? Map(StudySessionBLLDto? entity)
    {
        if (entity == null) return null;

        var res = new StudySessionDto()
        {
            Id = entity.Id,
            Description = entity.Description,
            Active = entity.Active,
            AssignmentId = entity.AssignmentId,
            RoomId = entity.RoomId
        };
        
        return res;
    }
}