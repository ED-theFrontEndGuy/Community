using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.DAL.Interfaces;
using Base.Interfaces;

namespace App.BLL.Services;

public class TripService : BaseService<TripBLLDto, TripDto, ITripRepository>, ITripService
{
    public TripService(
        IAppUOW serviceUOW,
        IMapper<TripBLLDto, TripDto, Guid> mapper) : base(serviceUOW, serviceUOW.TripRepository, mapper)
    {
    }
}
