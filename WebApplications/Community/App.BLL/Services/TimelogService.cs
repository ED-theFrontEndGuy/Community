using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

public class TimelogService : BaseService<TimelogBLLDto, TimelogDto, ITimelogRepository>, ITimelogService
{
    public TimelogService(
        IAppUOW serviceUOW,
        IBLLMapper<TimelogBLLDto, TimelogDto, Guid> bllMapper) : base(serviceUOW, serviceUOW.TimelogRepository, bllMapper)
    {
    }
}