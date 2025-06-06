namespace App.DTO.v1;

public class UserTripCreate
{
    public bool? IsUserTripAdmin { get; set; }
    
    public Guid TripId { get; set; }
}