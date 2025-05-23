using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

public class AttachmentService : BaseService<AttachmentBLLDto, AttachmentDto>, IAttachmentService
{
    public AttachmentService(IBaseUOW serviceUOW, IBaseRepository<AttachmentDto, Guid> serviceRepository, IBLLMapper<AttachmentBLLDto, AttachmentDto, Guid> bllMapper) : base(serviceUOW, serviceRepository, bllMapper)
    {
    }
}