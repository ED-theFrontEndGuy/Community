using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class UserTrip : BaseEntityUser<AppUser>
{
    public bool? IsUserTripAdmin { get; set; }
    
    public Guid TripId { get; set; }
    public Trip? Trip { get; set; }
}