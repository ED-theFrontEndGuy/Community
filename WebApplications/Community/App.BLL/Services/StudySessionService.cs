using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.Interfaces;

namespace App.BLL.Services;

public class StudySessionService : BaseService<StudySessionBLLDto, StudySessionDto, IStudySessionRepository>, IStudySessionService
{
    public StudySessionService(
        IAppUOW serviceUOW,
        IMapper<StudySessionBLLDto, StudySessionDto, Guid> mapper) : base(serviceUOW, serviceUOW.StudySessionRepository, mapper)
    {
    }
}