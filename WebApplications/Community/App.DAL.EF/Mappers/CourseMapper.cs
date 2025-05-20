using App.DAL.DTO;
using App.Domain;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class CourseMapper : IMapper<CourseDto, Course>
{
    public CourseDto? Map(Course? entity)
    {
        if (entity == null) return null;

        var res = new CourseDto()
        {
            Id = entity.Id,
            Name = entity.Name
        };

        return res;
    }

    public Course? Map(CourseDto? entity)
    {
        if (entity == null) return null;

        var res = new Course()
        {
            Id = entity.Id,
            Name = entity.Name
        };

        return res;
    }
}