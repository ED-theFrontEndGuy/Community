using Base.Domain;

namespace App.Domain;

public class Trip : BaseEntity
{
    public string Name { get; set; } = default!;
    public string? Destination { get; set; }
    public decimal? Budget { get; set; }
    public DateTime? DepartureDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsPublic { get; set; } = true;
    
    public ICollection<UserTrip>? UserTrips { get; set; }
    public ICollection<TripExpense>? TripExpenses { get; set; }
}