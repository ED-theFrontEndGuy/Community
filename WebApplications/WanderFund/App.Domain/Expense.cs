using System.ComponentModel.DataAnnotations;
using App.Domain.ValidationAttributes;
using Base.Domain;

namespace App.Domain;

public class Expense : BaseEntity
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;

    [MaxLength(256)]
    public string? ExpenseReference { get; set; }
    public decimal? ExpenseCost { get; set; }
    
    [MaxLength(3)]
    [CurrencyCodeValidation]
    public string? Currency { get; set; }

    public Guid? ExpenseCategoryId { get; set; }
    public ExpenseCategory? ExpenseCategory { get; set; }
    
    public ICollection<TripExpense>? TripExpenses { get; set; }
}