using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

public class StudyGroupUserService : BaseService<StudyGroupUserBLLDto, StudyGroupUserDto>, IStudyGroupUserService
{
    public StudyGroupUserService(IBaseUOW serviceUOW, IBaseRepository<StudyGroupUserDto, Guid> serviceRepository, IBLLMapper<StudyGroupUserBLLDto, StudyGroupUserDto, Guid> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}