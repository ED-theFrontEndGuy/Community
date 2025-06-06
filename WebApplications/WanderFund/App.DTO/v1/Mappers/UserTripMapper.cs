using App.BLL.DTO;
using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class UserTripMapper : IMapper<UserTrip, UserTripBLLDto>
{
    public UserTrip? Map(UserTripBLLDto? entity)
    {
        if (entity == null) return null;

        var res = new UserTrip()
        {
            Id = entity.Id,
            IsUserTripAdmin = entity.IsUserTripAdmin,
            TripId = entity.TripId,
        };

        return res;
    }

    public UserTripBLLDto? Map(UserTrip? entity)
    {
        if (entity == null) return null;

        var res = new UserTripBLLDto()
        {
            Id = entity.Id,
            IsUserTripAdmin = entity.IsUserTripAdmin,
            TripId = entity.TripId,
        };

        return res;
    }
    
    public UserTripBLLDto Map(UserTripCreate entity)
    {
        var res = new UserTripBLLDto()
        {
            Id = Guid.NewGuid(),
            IsUserTripAdmin = entity.IsUserTripAdmin,
            TripId = entity.TripId,
        };

        return res;
    }
}