using App.BLL.DTO;
using App.BLL.Interfaces;
using App.DAL.DTO;
using App.DAL.Interfaces;
using Base.BLL;
using Base.Interfaces;

namespace App.BLL.Services;

public class AttachmentService : BaseService<AttachmentBLLDto, AttachmentDto, IAttachmentRepository>, IAttachmentService
{
    public AttachmentService(
        IAppUOW serviceUOW,
        IMapper<AttachmentBLLDto, AttachmentDto, Guid> mapper) : base(serviceUOW, serviceUOW.AttachmentRepository, mapper)
    {
    }
}