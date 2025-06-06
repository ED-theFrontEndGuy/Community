using Base.Interfaces;

namespace App.DTO.v1;

public class Trip : IDomainId
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = default!;
    public string? Destination { get; set; }
    public decimal? Budget { get; set; }
    public DateTime? DepartureDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool IsPublic { get; set; } = true;
    
    public decimal? TripExpensesTotal { get; set; }
}