using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface IAppBLL : IBaseBLL
{
    // WanderFund
    ITripService TripService { get; }
    ITripExpenseService TripExpenseService { get; }
    IUserTripService UserTripService { get; }
    IExpenseService ExpenseService { get; }
    IExpenseCategoryService ExpenseCategoryService { get; }    
}