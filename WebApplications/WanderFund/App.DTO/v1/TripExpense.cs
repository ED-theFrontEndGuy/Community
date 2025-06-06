using Base.Interfaces;

namespace App.DTO.v1;

public class TripExpense : IDomainId
{
    public Guid Id { get; set; }
    
    public Guid TripId { get; set; }
    public Trip? Trip { get; set; }

    public Guid ExpenseId { get; set; }
}