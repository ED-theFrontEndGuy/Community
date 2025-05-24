using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

public class CourseService : BaseService<CourseBLLDto, CourseDto, ICourseRepository>, ICourseService
{
    public CourseService(
        IAppUOW serviceUOW,
        IBLLMapper<CourseBLLDto, CourseDto, Guid> bllMapper) : base(serviceUOW, serviceUOW.CourseRepository, bllMapper)
    {
    }
}