using Base.Interfaces;

namespace App.BLL.DTO;

public class TripExpenseBLLDto : IDomainId
{
    public Guid Id { get; set; }
    
    public Guid TripId { get; set; }
    public TripBLLDto? Trip { get; set; }

    public Guid ExpenseId { get; set; }
    public ExpenseBLLDto? Expense { get; set; }
}