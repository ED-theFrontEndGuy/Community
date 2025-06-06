using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface IAppUOW : IBaseUOW
{
    ITripRepository TripRepository { get; }
    ITripExpenseRepository TripExpenseRepository { get; }
    IUserTripRepository UserTripRepository { get; }
    IExpenseRepository ExpenseRepository { get; }
    IExpenseCategoryRepository ExpenseCategoryRepository { get; }
}