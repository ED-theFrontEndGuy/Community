using Base.Interfaces;

namespace App.DAL.DTO;

public class TripExpenseDto : IDomainId
{
    public Guid Id { get; set; }
    
    public Guid TripId { get; set; }
    public TripDto? Trip { get; set; }

    public Guid ExpenseId { get; set; }
    public ExpenseDto? Expense { get; set; }
}