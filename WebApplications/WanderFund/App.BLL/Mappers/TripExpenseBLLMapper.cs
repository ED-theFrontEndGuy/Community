using App.BLL.DTO;
using App.DAL.DTO;
using Base.Interfaces;

namespace App.BLL.Mappers;

public class TripExpenseBLLMapper : IMapper<TripExpenseBLLDto, TripExpenseDto>
{
    public TripExpenseBLLDto? Map(TripExpenseDto? entity)
    {
        if (entity == null) return null;

        var res = new TripExpenseBLLDto()
        {
            Id = entity.Id,
            TripId = entity.TripId,
            ExpenseId = entity.ExpenseId,
            Expense = entity.Expense == null
                ? null
                : new ExpenseBLLDto()
                {
                    ExpenseCost = entity.Expense.ExpenseCost,
                    Currency = entity.Expense.Currency,
                }
        };
        
        return res;
    }

    public TripExpenseDto? Map(TripExpenseBLLDto? entity)
    {
        if (entity == null) return null;
        
        var res = new TripExpenseDto()
        {
            Id = entity.Id,
            TripId = entity.TripId,
            ExpenseId = entity.ExpenseId,
        };
        
        return res;
    }
}