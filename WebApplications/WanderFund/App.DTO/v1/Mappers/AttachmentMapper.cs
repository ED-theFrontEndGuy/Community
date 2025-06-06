using App.BLL.DTO;
using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class AttachmentMapper : IMapper<Attachment, AttachmentBLLDto>
{
    public Attachment? Map(AttachmentBLLDto? entity)
    {
        if (entity == null) return null;
        
        var res = new Attachment()
        {
            Id = entity.Id,
            Link = entity.Link,
            Description = entity.Description,
            AssignmentId = entity.AssignmentId
        };

        return res;
    }

    public AttachmentBLLDto? Map(Attachment? entity)
    {
        if (entity == null) return null;
        
        var res = new AttachmentBLLDto()
        {
            Id = entity.Id,
            Link = entity.Link,
            Description = entity.Description,
            AssignmentId = entity.AssignmentId,
        };
        
        return res;
    }
    
    public AttachmentBLLDto Map(AttachmentCreate entity)
    {
        var res = new AttachmentBLLDto()
        {
            Id = Guid.NewGuid(),
            Link = entity.Link,
            Description = entity.Description,
            AssignmentId = entity.AssignmentId,
        };
        
        return res;
    }
}