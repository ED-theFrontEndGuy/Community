using App.BLL.DTO;
using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class ExpenseMapper : IMapper<Expense, ExpenseBLLDto>
{
    public Expense? Map(ExpenseBLLDto? entity)
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

    public ExpenseBLLDto? Map(Expense? entity)
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
        };

        return res;
    }
    
    public ExpenseBLLDto Map(ExpenseCreate entity)
    {
        var res = new ExpenseBLLDto()
        {
            Id = Guid.NewGuid(),
            Name = entity.Name,
            ExpenseReference = entity.ExpenseReference,
            ExpenseCost = entity.ExpenseCost,
            Currency = entity.Currency,
            ExpenseCategoryId = entity.ExpenseCategoryId,
        };

        return res;
    }
}