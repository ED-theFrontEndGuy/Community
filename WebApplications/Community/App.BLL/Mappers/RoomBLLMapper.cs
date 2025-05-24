using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Interfaces;

namespace App.BLL.Mappers;

public class RoomBLLMapper : IBLLMapper<RoomBLLDto, RoomDto>
{
    public RoomBLLDto? Map(RoomDto? entity)
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

    public RoomDto? Map(RoomBLLDto? entity)
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
}