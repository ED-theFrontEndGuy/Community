using App.BLL.DTO;
using App.DAL.DTO;
using Base.Interfaces;

namespace App.BLL.Mappers;

public class UserTripBLLMapper : IMapper<UserTripBLLDto, UserTripDto>
{
    public UserTripBLLDto? Map(UserTripDto? entity)
    {
        if (entity == null) return null;

        var res = new UserTripBLLDto()
        {
            Id = entity.Id,
            IsUserTripAdmin = entity.IsUserTripAdmin,
            TripId = entity.TripId,
            Trip = entity.Trip == null
                ? null
                : new TripBLLDto()
                {
                    Id = entity.Trip.Id,
                    Name = entity.Trip.Name,
                }
        };

        return res;
    }

    public UserTripDto? Map(UserTripBLLDto? entity)
    {
        if (entity == null) return null;
        
        var res = new UserTripDto()
        {
            Id = entity.Id,
            IsUserTripAdmin = entity.IsUserTripAdmin,
            TripId = entity.TripId,
        };
        
        return res;
    }
}