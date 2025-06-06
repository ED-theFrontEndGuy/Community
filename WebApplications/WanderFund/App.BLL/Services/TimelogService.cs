using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.Interfaces;

namespace App.BLL.Services;

public class TimelogService : BaseService<TimelogBLLDto, TimelogDto, ITimelogRepository>, ITimelogService
{
    public TimelogService(
        IAppUOW serviceUOW,
        IMapper<TimelogBLLDto, TimelogDto, Guid> mapper) : base(serviceUOW, serviceUOW.TimelogRepository, mapper)
    {
    }
}