using App.BLL.Interfaces;
using App.BLL.Mappers;
using App.BLL.Services;
using App.DAL.Interfaces;
using Base.BLL;

namespace App.BLL;

public class AppBLL : BaseBLL<IAppUOW>, IAppBLL
{
    public AppBLL(IAppUOW uow) : base(uow)
    {
    }
    
    private ITripService? _tripService;
    public ITripService TripService =>
        _tripService ??= new TripService(
            BLLUOW,
            new TripBLLMapper());
    
    private ITripExpenseService? _tripExpenseService;
    public ITripExpenseService TripExpenseService =>
        _tripExpenseService ??= new TripExpenseService(
            BLLUOW,
            new TripExpenseBLLMapper());
    
    private IUserTripService? _userTripService;
    public IUserTripService UserTripService =>
        _userTripService ??= new UserTripService(
            BLLUOW,
            new UserTripBLLMapper());
    
    private IExpenseService? _expenseService;
    public IExpenseService ExpenseService =>
        _expenseService ??= new ExpenseService(
            BLLUOW,
            new ExpenseBLLMapper());
    
    private IExpenseCategoryService? _expenseCategoryService;
    public IExpenseCategoryService ExpenseCategoryService =>
        _expenseCategoryService ??= new ExpenseCategoryService(
            BLLUOW,
            new ExpenseCategoryBLLMapper());
}