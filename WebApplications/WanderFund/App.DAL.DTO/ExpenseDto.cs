using System.ComponentModel.DataAnnotations;
using App.Domain.ValidationAttributes;
using Base.Interfaces;

namespace App.DAL.DTO;

public class ExpenseDto : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(128)]
    public string Name { get; set; } = default!;

    [MaxLength(256)]
    public string? ExpenseReference { get; set; }
    public decimal? ExpenseCost { get; set; }
    
    [MaxLength(3)]
    [CurrencyCodeValidation]
    public string? Currency { get; set; }

    public Guid? ExpenseCategoryId { get; set; }
    public ExpenseCategoryDto? ExpenseCategory { get; set; }
    
    public ICollection<TripExpenseDto>? TripExpenses { get; set; }
}