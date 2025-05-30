using App.BLL.DTO;
using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class RoomMapper : IMapper<Room, RoomBLLDto>
{
    public Room? Map(RoomBLLDto? entity)
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

    public RoomBLLDto? Map(Room? entity)
    {
        if (entity == null) return null;
        
        var res = new RoomBLLDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
        };

        return res;
    }
    
    public RoomBLLDto Map(RoomCreate entity)
    {
        var res = new RoomBLLDto()
        {
            Id = Guid.NewGuid(),
            Name = entity.Name,
            Description = entity.Description,
        };

        return res;
    }
}