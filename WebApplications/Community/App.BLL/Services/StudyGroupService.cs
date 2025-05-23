using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

public class StudyGroupService : BaseService<StudyGroupBLLDto, StudyGroupDto>, IStudyGroupService
{
    public StudyGroupService(IBaseUOW serviceUOW, IBaseRepository<StudyGroupDto, Guid> serviceRepository, IBLLMapper<StudyGroupBLLDto, StudyGroupDto, Guid> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}