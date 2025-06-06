using Base.Interfaces;

namespace App.DTO.v1;

public class UserTrip : IDomainId
{
    public Guid Id { get; set; }
    
    public bool? IsUserTripAdmin { get; set; }
    
    public Guid TripId { get; set; }
}