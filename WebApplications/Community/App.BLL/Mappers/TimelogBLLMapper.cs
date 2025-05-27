using App.BLL.DTO;
using App.DAL.DTO;
using Base.Interfaces;

namespace App.BLL.Mappers;

public class TimelogBLLMapper : IMapper<TimelogBLLDto, TimelogDto>
{
    public TimelogBLLDto? Map(TimelogDto? entity)
    {
        if (entity == null) return null;

        var res = new TimelogBLLDto()
        {
            Id = entity.Id,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            Duration = entity.Duration,
            DeclarationId = entity.DeclarationId,
            Declaration = entity.Declaration == null
                ? null
                : new DeclarationBLLDto()
                {
                    Id = entity.Declaration.Id,
                    Active = entity.Declaration.Active,
                    CourseId = entity.Declaration.CourseId,
                    Course = entity.Declaration.Course == null
                        ? null
                        : new CourseBLLDto()
                        {
                            Id = entity.Declaration.Course.Id,
                            Name = entity.Declaration.Course.Name,
                        }
                },
            AssignmentId = entity.AssignmentId,
            Assignment = entity.Assignment == null
                ? null
                : new AssignmentBLLDto()
                {
                    Id = entity.Assignment.Id,
                    Name = entity.Assignment.Name,
                }
        };

        return res;
    }

    public TimelogDto? Map(TimelogBLLDto? entity)
    {
        if (entity == null) return null;

        var res = new TimelogDto()
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
}