namespace App.DTO.v1;

public class TripExpenseCreate
{
    public Guid TripId { get; set; }
    public Trip? Trip { get; set; }

    public Guid ExpenseId { get; set; }
}