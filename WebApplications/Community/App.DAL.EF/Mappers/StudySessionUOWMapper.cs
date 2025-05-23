using App.DAL.DTO;
using App.Domain;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class StudySessionUOWMapper : IUOWMapper<StudySessionDto, StudySession>
{
    public StudySessionDto? Map(StudySession? entity)
    {
        if (entity == null) return null;

        var res = new StudySessionDto()
        {
            Id = entity.Id,
            Description = entity.Description,
            Active = entity.Active,
            AssignmentId = entity.AssignmentId,
            Assignment = entity.Assignment == null
                ? null
                : new AssignmentDto()
                {
                    Id = entity.Assignment.Id,
                    Name = entity.Assignment.Name,
                },
            RoomId = entity.RoomId,
            Room = entity.Room == null
                ? null
                : new RoomDto()
                {
                    Id = entity.Room.Id,
                    Name = entity.Room.Name,
                },
            StudyGroup = entity.StudyGroup == null
                ? null
                : new StudyGroupDto()
                {
                    Id = entity.StudyGroup.Id,
                    Name = entity.StudyGroup.Name,
                }
        };

        return res;
    }

    public StudySession? Map(StudySessionDto? entity)
    {
        if (entity == null) return null;

        var res = new StudySession()
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