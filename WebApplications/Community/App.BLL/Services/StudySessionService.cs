using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

public class StudySessionService : BaseService<StudySessionBLLDto, StudySessionDto, IStudySessionRepository>, IStudySessionService
{
    public StudySessionService(
        IAppUOW serviceUOW,
        IBLLMapper<StudySessionBLLDto, StudySessionDto, Guid> bllMapper) : base(serviceUOW, serviceUOW.StudySessionRepository, bllMapper)
    {
    }
}