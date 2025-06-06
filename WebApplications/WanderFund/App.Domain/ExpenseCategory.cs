using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class ExpenseCategory : BaseEntityUser<AppUser>
{
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    [MaxLength(256)]
    public string? Description { get; set; }

    public ICollection<Expense>? Expenses { get; set; }
}