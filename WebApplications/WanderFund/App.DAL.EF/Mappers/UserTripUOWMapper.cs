using App.DAL.DTO;
using App.Domain;
using Base.Interfaces;

namespace App.DAL.EF.Mappers;

public class UserTripUOWMapper : IMapper<UserTripDto, UserTrip>
{
    public UserTripDto? Map(UserTrip? entity)
    {
        if (entity == null) return null;

        var res = new UserTripDto()
        {
            Id = entity.Id,
            IsUserTripAdmin = entity.IsUserTripAdmin,
            TripId = entity.TripId,
            Trip = entity.Trip == null
            ? null
            : new TripDto()
            {
                Id = entity.Trip.Id,
                Name = entity.Trip.Name,
            }
        };

        return res;
    }

    public UserTrip? Map(UserTripDto? entity)
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
}