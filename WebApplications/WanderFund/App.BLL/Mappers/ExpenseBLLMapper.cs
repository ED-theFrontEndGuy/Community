using App.BLL.DTO;
using App.DAL.DTO;
using Base.Interfaces;

namespace App.BLL.Mappers;

public class ExpenseBLLMapper : IMapper<ExpenseBLLDto, ExpenseDto>
{
    public ExpenseBLLDto? Map(ExpenseDto? entity)
    {
        if (entity == null) return null;
        
        var res = new ExpenseBLLDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            ExpenseReference = entity.ExpenseReference,
            ExpenseCost = entity.ExpenseCost,
            Currency = entity.Currency,
            ExpenseCategoryId = entity.ExpenseCategoryId,
            ExpenseCategory = entity.ExpenseCategory == null
                ? null
                : new ExpenseCategoryBLLDto()
                {
                    Id = entity.ExpenseCategory.Id,
                    Name = entity.ExpenseCategory.Name,
                    Description = entity.ExpenseCategory.Description,
                }
        };
        
        return res;
    }

    public ExpenseDto? Map(ExpenseBLLDto? entity)
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
        };
        
        return res;
    }
}