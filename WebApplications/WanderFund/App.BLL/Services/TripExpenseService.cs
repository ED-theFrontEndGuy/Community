using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.Interfaces;

namespace App.BLL.Services;

public class TripExpenseService : BaseService<TripExpenseBLLDto, TripExpenseDto, ITripExpenseRepository>, ITripExpenseService
{
    public TripExpenseService(
        IAppUOW serviceUOW,
        IMapper<TripExpenseBLLDto, TripExpenseDto, Guid> mapper) : base(serviceUOW, serviceUOW.TripExpenseRepository, mapper)
    {
    }
}