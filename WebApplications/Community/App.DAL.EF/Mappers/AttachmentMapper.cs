using App.DAL.DTO;
using App.Domain;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class AttachmentMapper : IMapper<AttachmentDto, Attachment>
{
    public AttachmentDto? Map(Attachment? entity)
    {
        if (entity == null) return null;
        
        var res = new AttachmentDto()
        {
            Id = entity.Id,
            Link = entity.Link,
            Description = entity.Description,
            AssignmentId = entity.AssignmentId,
            Assignment = entity.Assignment == null
                ? null
                : new AssignmentDto()
                {
                    Id = entity.Assignment.Id,
                    Name = entity.Assignment.Name,
                }
        };

        return res;
    }

    public Attachment? Map(AttachmentDto? entity)
    {
        if (entity == null) return null;
        
        var res = new Attachment()
        {
            Id = entity.Id,
            Link = entity.Link,
            Description = entity.Description,
            AssignmentId = entity.AssignmentId,
        };
        
        return res;
    }
}