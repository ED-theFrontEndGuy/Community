using App.DAL.DTO;
using App.Domain;

using Base.DAL.Interfaces;
namespace App.DAL.EF.Mappers;

public class RoomMapper : IMapper<RoomDto, Room>
{
    public RoomDto? Map(Room? entity)
    {
        throw new NotImplementedException();
    }

    public Room? Map(RoomDto? entity)
    {
        throw new NotImplementedException();
    }
}