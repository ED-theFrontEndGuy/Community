using App.BLL.DTO;
using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class TripExpenseMapper : IMapper<TripExpense, TripExpenseBLLDto>
{
    public TripExpense? Map(TripExpenseBLLDto? entity)
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

    public TripExpenseBLLDto? Map(TripExpense? entity)
    {
        if (entity == null) return null;

        var res = new TripExpenseBLLDto()
        {
            Id = entity.Id,
            TripId = entity.TripId,
            ExpenseId = entity.ExpenseId,
        };

        return res;
    }
    
    public TripExpenseBLLDto Map(TripExpenseCreate entity)
    {
        var res = new TripExpenseBLLDto()
        {
            Id = Guid.NewGuid(),
            TripId = entity.TripId,
            ExpenseId = entity.ExpenseId,
        };

        return res;
    }
}