using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.Interfaces;

namespace App.BLL.Services;

public class ExpenseCategoryService : BaseService<ExpenseCategoryBLLDto, ExpenseCategoryDto, IExpenseCategoryRepository>, IExpenseCategoryService
{
    public ExpenseCategoryService(
        IAppUOW serviceUOW,
        IMapper<ExpenseCategoryBLLDto, ExpenseCategoryDto, Guid> mapper) : base(serviceUOW, serviceUOW.ExpenseCategoryRepository, mapper)
    {
    }
}