using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.DAL.DTO;

public class ExpenseCategoryDto : IDomainId
{
    public Guid Id { get; set; }
    
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    [MaxLength(256)]
    public string? Description { get; set; }

    public ICollection<ExpenseDto>? Expenses { get; set; }
}