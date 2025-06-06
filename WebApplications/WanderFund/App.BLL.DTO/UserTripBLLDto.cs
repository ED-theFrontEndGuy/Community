using Base.Interfaces;

namespace App.BLL.DTO;

public class UserTripBLLDto : IDomainId
{
    public Guid Id { get; set; }
    
    public bool? IsUserTripAdmin { get; set; }
    
    public Guid TripId { get; set; }
    public TripBLLDto? Trip { get; set; }
}