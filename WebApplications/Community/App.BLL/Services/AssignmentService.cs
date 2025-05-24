using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;

namespace App.BLL.Services;

public class AssignmentService : BaseService<AssignmentBLLDto, AssignmentDto, IAssignmentRepository>, IAssignmentService
{
    public AssignmentService(
        IAppUOW serviceUOW, 
        IBLLMapper<AssignmentBLLDto, AssignmentDto, Guid> bllMapper) : base(serviceUOW, serviceUOW.AssignmentRepository, bllMapper)
    {
    }
} 