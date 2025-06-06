using App.DAL.DTO;
using App.Domain;
using Base.Interfaces;

namespace App.DAL.EF.Mappers;

public class ExpenseUOWMapper : IMapper<ExpenseDto, Expense>
{
    public ExpenseDto? Map(Expense? entity)
    {
        if (entity == null) return null;
        
        var res = new ExpenseDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            ExpenseReference = entity.ExpenseReference,
            ExpenseCost = entity.ExpenseCost,
            Currency = entity.Currency,
            ExpenseCategoryId = entity.ExpenseCategoryId,
            ExpenseCategory = entity.ExpenseCategory == null
                ? null
                : new ExpenseCategoryDto()
                {
                    Id = entity.ExpenseCategory.Id,
                    Name = entity.ExpenseCategory.Name,
                    Description = entity.ExpenseCategory.Description,
                }
        };
        
        return res;
    }

    public Expense? Map(ExpenseDto? entity)
    {
        if (entity == null) return null;

        var res = new Expense()
        {
            Id = entity.Id,
            Name = entity.Name,
            ExpenseReference = entity.ExpenseReference,
            ExpenseCost = entity.ExpenseCost,
            Currency = entity.Currency,
            ExpenseCategoryId = entity.ExpenseCategoryId,
        };
        
        return res;
    }
}