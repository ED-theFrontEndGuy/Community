using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

public class TimelogService : BaseService<TimelogBLLDto, TimelogDto>, ITimelogService
{
    public TimelogService(IBaseUOW serviceUOW, IBaseRepository<TimelogDto, Guid> serviceRepository, IBLLMapper<TimelogBLLDto, TimelogDto, Guid> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}