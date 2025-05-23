using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Interfaces;

namespace App.BLL.Mappers;

public class CourseBLLMapper : IBLLMapper<CourseBLLDto, CourseDto>
{
    public CourseBLLDto? Map(CourseDto? entity)
    {
        throw new NotImplementedException();
    }

    public CourseDto? Map(CourseBLLDto? entity)
    {
        throw new NotImplementedException();
    }
}