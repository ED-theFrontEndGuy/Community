using App.BLL.DTO;
using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class TimelogMapper : IMapper<Timelog, TimelogBLLDto>
{
    public Timelog? Map(TimelogBLLDto? entity)
    {
        if (entity == null) return null;

        var res = new Timelog()
        {
            Id = entity.Id,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            Duration = entity.Duration,
            DeclarationId = entity.DeclarationId,
            CourseName = (entity.Declaration == null
                ? null
                : entity.Declaration!.Course!.Name)!,
            AssignmentId = entity.AssignmentId,
            AssignmentName = entity.Assignment?.Name!
        };

        return res;
    }

    public TimelogBLLDto? Map(Timelog? entity)
    {
        if (entity == null) return null;

        var res = new TimelogBLLDto()
        {
            Id = entity.Id,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            Duration = entity.Duration,
            DeclarationId = entity.DeclarationId,
            AssignmentId = entity.AssignmentId,
        };

        return res;
    }
    
    public TimelogBLLDto Map(TimelogCreate entity)
    {
        var res = new TimelogBLLDto()
        {
            Id = Guid.NewGuid(),
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            Duration = entity.Duration,
            DeclarationId = entity.DeclarationId,
            AssignmentId = entity.AssignmentId,
        };

        return res;
    }
}