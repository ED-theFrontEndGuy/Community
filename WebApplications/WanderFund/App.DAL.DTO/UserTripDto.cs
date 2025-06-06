using Base.Interfaces;

namespace App.DAL.DTO;

public class UserTripDto : IDomainId
{
    public Guid Id { get; set; }
    
    public bool? IsUserTripAdmin { get; set; }
    
    public Guid TripId { get; set; }
    public TripDto? Trip { get; set; }
}