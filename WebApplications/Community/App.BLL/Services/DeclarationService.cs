using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

public class DeclarationService : BaseService<DeclarationBLLDto, DeclarationDto>, IDeclarationService
{
    public DeclarationService(IBaseUOW serviceUOW, IBaseRepository<DeclarationDto, Guid> serviceRepository, IBLLMapper<DeclarationBLLDto, DeclarationDto, Guid> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}