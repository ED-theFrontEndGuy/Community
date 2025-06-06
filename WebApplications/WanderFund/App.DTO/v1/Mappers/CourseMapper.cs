using App.BLL.DTO;
using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class CourseMapper : IMapper<Course, CourseBLLDto>
{
    public Course? Map(CourseBLLDto? entity)
    {
        if (entity == null) return null;

        var res = new Course()
        {
            Id = entity.Id,
            Name = entity.Name
        };

        return res;
    }

    public CourseBLLDto? Map(Course? entity)
    {
        if (entity == null) return null;

        var res = new CourseBLLDto()
        {
            Id = entity.Id,
            Name = entity.Name
        };

        return res;
    }
    
    public CourseBLLDto Map(CourseCreate entity)
    {
        var res = new CourseBLLDto()
        {
            Id = Guid.NewGuid(),
            Name = entity.Name
        };
        
        return res;
    }
}