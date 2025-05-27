using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.Interfaces;

namespace App.BLL.Services;

public class RoomService : BaseService<RoomBLLDto, RoomDto, IRoomRepository>, IRoomService
{
    public RoomService(
        IAppUOW serviceUOW, 
        IMapper<RoomBLLDto, RoomDto, Guid> mapper) : base(serviceUOW, serviceUOW.RoomRepository, mapper)
    {
    }
}