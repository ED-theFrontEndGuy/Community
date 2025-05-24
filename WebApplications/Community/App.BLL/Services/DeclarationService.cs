using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

public class DeclarationService : BaseService<DeclarationBLLDto, DeclarationDto, IDeclarationRepository>, IDeclarationService
{
    public DeclarationService(
        IAppUOW serviceUOW,
        IBLLMapper<DeclarationBLLDto, DeclarationDto, Guid> bllMapper) : base(serviceUOW, serviceUOW.DeclarationRepository, bllMapper)
    {
    }
}