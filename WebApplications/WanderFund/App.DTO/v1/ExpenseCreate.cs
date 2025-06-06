using System.ComponentModel.DataAnnotations;

namespace App.DTO.v1;

public class ExpenseCreate
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;

    [MaxLength(256)]
    public string? ExpenseReference { get; set; }
    public decimal? ExpenseCost { get; set; }
    
    [MaxLength(3)]
    public string? Currency { get; set; }

    public Guid? ExpenseCategoryId { get; set; }
}