using System.ComponentModel.DataAnnotations;
using App.Domain.ValidationAttributes;
using Base.Domain.Identity;

namespace App.Domain.Identity;

public class AppUser : BaseUser<AppUserRole>
{
    [MinLength(1)]
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;
    
    [MinLength(1)]
    [MaxLength(128)]
    public string LastName { get; set; } = default!;
    
    [MaxLength(3)]
    [CurrencyCodeValidation]
    public string? DefaultCurrencyCode { get; set; }
    
    // [Display(Name = nameof(Declarations), ResourceType = typeof(App.Resources.Domain.User))]
    public ICollection<AppRefreshToken>? AppRefreshTokens { get; set; }
    public ICollection<UserTrip>? UserTrips { get; set; }
    public ICollection<ExpenseCategory>? ExpenseCategories { get; set; }
}