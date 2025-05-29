using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Interfaces;
using Base.Interfaces;

namespace App.BLL.Mappers;

public class CourseBLLMapper : IMapper<CourseBLLDto, CourseDto>
{
    public CourseBLLDto? Map(CourseDto? entity)
    {
        if (entity == null) return null;

        var res = new CourseBLLDto()
        {
            Id = entity.Id,
            Name = entity.Name
        };

        return res;
    }

    public CourseDto? Map(CourseBLLDto? entity)
    {
        if (entity == null) return null;

        var res = new CourseDto()
        {
            Id = entity.Id,
            Name = entity.Name
        };

        return res;
    }
}