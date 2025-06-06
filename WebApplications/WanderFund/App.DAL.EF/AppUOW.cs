using App.DAL.EF.Repositories;
using App.DAL.Interfaces;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUOW<AppDbContext>, IAppUOW
{
    public AppUOW(AppDbContext uowDbContext) : base(uowDbContext)
    {
    }
    
    // simple caching
   // WanderFund
    private ITripRepository? _tripRepository;
    public ITripRepository TripRepository =>
        _tripRepository ??= new TripRepository(UOWDbContext);

    private ITripExpenseRepository? _tripExpenseRepository;
    public ITripExpenseRepository TripExpenseRepository =>
        _tripExpenseRepository ??= new TripExpenseRepository(UOWDbContext);

    private IUserTripRepository? _userTripRepository;
    public IUserTripRepository UserTripRepository =>
        _userTripRepository ??= new UserTripRepository(UOWDbContext);
    
    private IExpenseRepository? _expenseRepository;
    public IExpenseRepository ExpenseRepository =>
        _expenseRepository ??= new ExpenseRepository(UOWDbContext);
    
    private IExpenseCategoryRepository? _expenseCategoryRepository;
    public IExpenseCategoryRepository ExpenseCategoryRepository =>
        _expenseCategoryRepository ??= new ExpenseCategoryRepository(UOWDbContext);
}