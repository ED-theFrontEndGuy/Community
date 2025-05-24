using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

// public class StudyGroupUserService : BaseService<StudyGroupUserBLLDto, StudyGroupUserDto, IStudyGroupUserRepository>, IStudyGroupUserService
// {
//     public StudyGroupUserService(
//         IAppUOW serviceUOW, 
//         IBLLMapper<StudyGroupUserBLLDto, StudyGroupUserDto, Guid> bllMapper) : base(serviceUOW, serviceUOW.StudyGroupUserRepository, bllMapper)
//     {
//     }
// }