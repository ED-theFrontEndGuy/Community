using App.DAL.DTO;
using App.Domain;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class AttachmentMapper : IMapper<AttachmentDto, Attachment>
{
    public AttachmentDto? Map(Attachment? entity)
    {
        throw new NotImplementedException();
    }

    public Attachment? Map(AttachmentDto? entity)
    {
        throw new NotImplementedException();
    }
}