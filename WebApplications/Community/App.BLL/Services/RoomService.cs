using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

public class RoomService : BaseService<RoomBLLDto, RoomDto, IRoomRepository>, IRoomService
{
    public RoomService(
        IAppUOW serviceUOW, 
        IBLLMapper<RoomBLLDto, RoomDto, Guid> bllMapper) : base(serviceUOW, serviceUOW.RoomRepository, bllMapper)
    {
    }
}