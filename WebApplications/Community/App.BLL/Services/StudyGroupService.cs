using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

public class StudyGroupService : BaseService<StudyGroupBLLDto, StudyGroupDto, IStudyGroupRepository>, IStudyGroupService
{
    public StudyGroupService(
        IAppUOW serviceUOW,
        IBLLMapper<StudyGroupBLLDto, StudyGroupDto, Guid> bllMapper) : base(serviceUOW, serviceUOW.StudyGroupRepository, bllMapper)
    {
    }
}