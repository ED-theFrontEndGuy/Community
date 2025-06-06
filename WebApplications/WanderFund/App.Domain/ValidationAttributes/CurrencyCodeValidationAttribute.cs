namespace App.Domain.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;


public class CurrencyCodeValidationAttribute : ValidationAttribute

{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var code = value as string;
        if (string.IsNullOrEmpty(code))
        {
            return ValidationResult.Success;
        }
        bool isValid = false;
        try
        {
            // Safely filter cultures with regions
            var isoCodes = CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                .Select(c =>
                {
                    try { return new RegionInfo(c.LCID); }
                    catch { return null; }
                })
                .Where(r => r != null)
                .Select(r => r!.ISOCurrencySymbol)
                .Distinct(StringComparer.OrdinalIgnoreCase);

            isValid = isoCodes.Contains(code, StringComparer.OrdinalIgnoreCase);
        }
        catch
        {
            return new ValidationResult("Could not validate the currency code due to an unexpected error.");
        }

        if (!isValid)
        {
            return new ValidationResult($"'{code}' is not a valid ISO 4217 currency code.");
        }

        return ValidationResult.Success;
    }

}