using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.BLL.DTO;

public class ExpenseBLLDto : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(128)]
    public string Name { get; set; } = default!;

    [MaxLength(256)]
    public string? ExpenseReference { get; set; }
    public decimal? ExpenseCost { get; set; }
    
    [MaxLength(3)]
    public string? Currency { get; set; }

    public Guid? ExpenseCategoryId { get; set; }
    public ExpenseCategoryBLLDto? ExpenseCategory { get; set; }
    
    public ICollection<TripExpenseBLLDto>? TripExpenses { get; set; }
}