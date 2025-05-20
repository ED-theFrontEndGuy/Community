using App.DAL.DTO;
using App.Domain;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class TimelogMapper : IMapper<TimelogDto, Timelog>
{
    public TimelogDto? Map(Timelog? entity)
    {
        if (entity == null) return null;

        var res = new TimelogDto()
        {
            Id = entity.Id,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            Duration = entity.Duration,
            DeclarationId = entity.DeclarationId,
            Declaration = entity.Declaration == null
                ? null
                : new DeclarationDto()
                {
                    Id = entity.Declaration.Id,
                    Active = entity.Declaration.Active,
                    CourseId = entity.Declaration.CourseId,
                    Course = entity.Declaration.Course == null
                        ? null
                        : new CourseDto()
                        {
                            Id = entity.Declaration.Course.Id,
                            Name = entity.Declaration.Course.Name,
                        }
                },
            AssignmentId = entity.AssignmentId,
            Assignment = entity.Assignment == null
                ? null
                : new AssignmentDto()
                {
                    Id = entity.Assignment.Id,
                    Name = entity.Assignment.Name,
                }
        };

        return res;
    }

    public Timelog? Map(TimelogDto? entity)
    {
        if (entity == null) return null;

        var res = new Timelog()
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