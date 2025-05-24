using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.BLL.Interfaces;
using Base.DAL.Interfaces;

namespace App.BLL.Services;

public class AttachmentService : BaseService<AttachmentBLLDto, AttachmentDto, IAttachmentRepository>, IAttachmentService
{
    public AttachmentService(
        IAppUOW serviceUOW,
        IBLLMapper<AttachmentBLLDto, AttachmentDto, Guid> bllMapper) : base(serviceUOW, serviceUOW.AttachmentRepository, bllMapper)
    {
    }
}