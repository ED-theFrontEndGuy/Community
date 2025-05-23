using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

public class AssignmentService : BaseService<AssignmentBLLDto, AssignmentDto>, IAssignmentService
{
    public AssignmentService(
        IBaseUOW serviceUOW, 
        IBaseRepository<AssignmentDto, Guid> serviceRepository,
        IBLLMapper<AssignmentBLLDto, AssignmentDto, Guid> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}