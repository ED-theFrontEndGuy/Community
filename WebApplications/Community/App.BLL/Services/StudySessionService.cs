using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

public class StudySessionService : BaseService<StudySessionBLLDto, StudySessionDto>, IStudySessionService
{
    public StudySessionService(IBaseUOW serviceUOW, IBaseRepository<StudySessionDto, Guid> serviceRepository, IBLLMapper<StudySessionBLLDto, StudySessionDto, Guid> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}