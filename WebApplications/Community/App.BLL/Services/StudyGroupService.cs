using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.Interfaces;

namespace App.BLL.Services;

public class StudyGroupService : BaseService<StudyGroupBLLDto, StudyGroupDto, IStudyGroupRepository>, IStudyGroupService
{
    public StudyGroupService(
        IAppUOW serviceUOW,
        IMapper<StudyGroupBLLDto, StudyGroupDto, Guid> mapper) : base(serviceUOW, serviceUOW.StudyGroupRepository, mapper)
    {
    }
}