using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.DAL.Interfaces;
using Base.Interfaces;


namespace App.BLL.Services;

public class UserTripService : BaseService<UserTripBLLDto, UserTripDto, IUserTripRepository>, IUserTripService
{
    public UserTripService(
        IAppUOW serviceUOW,
        IMapper<UserTripBLLDto, UserTripDto, Guid> mapper) : base(serviceUOW, serviceUOW.UserTripRepository, mapper)
    {
    }
}