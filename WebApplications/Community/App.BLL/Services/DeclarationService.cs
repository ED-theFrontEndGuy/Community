using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.Interfaces;

namespace App.BLL.Services;

public class DeclarationService : BaseService<DeclarationBLLDto, DeclarationDto, IDeclarationRepository>, IDeclarationService
{
    public DeclarationService(
        IAppUOW serviceUOW,
        IMapper<DeclarationBLLDto, DeclarationDto, Guid> mapper) : base(serviceUOW, serviceUOW.DeclarationRepository, mapper)
    {
    }
}