using Base.Domain;

namespace App.Domain;

public class TripExpense : BaseEntity
{
    public Guid TripId { get; set; }
    public Trip? Trip { get; set; }

    public Guid ExpenseId { get; set; }
    public Expense? Expense { get; set; }
}