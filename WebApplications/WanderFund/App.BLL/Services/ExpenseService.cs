using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.Interfaces;

namespace App.BLL.Services;

public class ExpenseService : BaseService<ExpenseBLLDto, ExpenseDto, IExpenseRepository>, IExpenseService
{
    public ExpenseService(
        IAppUOW serviceUOW,
        IMapper<ExpenseBLLDto, ExpenseDto, Guid> mapper) : base(serviceUOW, serviceUOW.ExpenseRepository, mapper)
    {
    }
}