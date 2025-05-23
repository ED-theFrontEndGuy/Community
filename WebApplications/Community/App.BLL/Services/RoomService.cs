using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

public class RoomService : BaseService<RoomBLLDto, RoomDto>, IRoomService
{
    public RoomService(IBaseUOW serviceUOW, IBaseRepository<RoomDto, Guid> serviceRepository, IBLLMapper<RoomBLLDto, RoomDto, Guid> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}