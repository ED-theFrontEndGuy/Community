using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

public class CourseService : BaseService<CourseBLLDto, CourseDto>, ICourseService
{
    public CourseService(IBaseUOW serviceUOW, IBaseRepository<CourseDto, Guid> serviceRepository, IBLLMapper<CourseBLLDto, CourseDto, Guid> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}