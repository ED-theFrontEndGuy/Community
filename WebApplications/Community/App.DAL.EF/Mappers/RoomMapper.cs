using App.DAL.DTO;
using App.Domain;

using Base.DAL.Interfaces;
namespace App.DAL.EF.Mappers;

public class RoomMapper : IMapper<RoomDto, Room>
{
    public RoomDto? Map(Room? entity)
    {
        if (entity == null) return null;
        
        var res = new RoomDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
        };
        
        return res;
    }

    public Room? Map(RoomDto? entity)
    {
        if (entity == null) return null;
        
        var res = new Room()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
        };

        return res;
    }
}