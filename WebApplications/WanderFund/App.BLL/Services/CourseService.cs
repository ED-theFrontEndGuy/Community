using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.Interfaces;

namespace App.BLL.Services;

public class CourseService : BaseService<CourseBLLDto, CourseDto, ICourseRepository>, ICourseService
{
    public CourseService(
        IAppUOW serviceUOW,
        IMapper<CourseBLLDto, CourseDto, Guid> mapper) : base(serviceUOW, serviceUOW.CourseRepository, mapper)
    {
    }
}