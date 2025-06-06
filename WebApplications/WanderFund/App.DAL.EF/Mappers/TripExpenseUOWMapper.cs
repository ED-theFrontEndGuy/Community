using App.DAL.DTO;
using App.Domain;
using Base.Interfaces;

namespace App.DAL.EF.Mappers;

public class TripExpenseUOWMapper : IMapper<TripExpenseDto, TripExpense>
{
    public TripExpenseDto? Map(TripExpense? entity)
    {
        if (entity == null) return null;

        var res = new TripExpenseDto()
        {
            Id = entity.Id,
            TripId = entity.TripId,
            ExpenseId = entity.ExpenseId,
            Expense = entity.Expense == null
                ? null
                : new ExpenseDto()
                {
                    ExpenseCost = entity.Expense.ExpenseCost,
                    Currency = entity.Expense.Currency,
                }
        };
        
        return res;
    }

    public TripExpense? Map(TripExpenseDto? entity)
    {
        if (entity == null) return null;
        
        var res = new TripExpense()
        {
            Id = entity.Id,
            TripId = entity.TripId,
            ExpenseId = entity.ExpenseId,
        };
        
        return res;
    }
}