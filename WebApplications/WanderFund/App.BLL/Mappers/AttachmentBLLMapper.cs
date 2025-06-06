using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Interfaces;
using Base.Interfaces;

namespace App.BLL.Mappers;

public class AttachmentBLLMapper : IMapper<AttachmentBLLDto, AttachmentDto>
{
    public AttachmentBLLDto? Map(AttachmentDto? entity)
    {
        if (entity == null) return null;
        
        var res = new AttachmentBLLDto()
        {
            Id = entity.Id,
            Link = entity.Link,
            Description = entity.Description,
            AssignmentId = entity.AssignmentId,
            Assignment = entity.Assignment == null
                ? null
                : new AssignmentBLLDto()
                {
                    Id = entity.Assignment.Id,
                    Name = entity.Assignment.Name,
                }
        };

        return res;
    }

    public AttachmentDto? Map(AttachmentBLLDto? entity)
    {
        if (entity == null) return null;
        
        var res = new AttachmentDto()
        {
            Id = entity.Id,
            Link = entity.Link,
            Description = entity.Description,
            AssignmentId = entity.AssignmentId,
        };
        
        return res;
    }
}