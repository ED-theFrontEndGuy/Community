using App.BLL.DTO;
using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class AssignmentMapper : IMapper<Assignment, AssignmentBLLDto>
{
    public Assignment? Map(AssignmentBLLDto? entity)
    {
        if (entity == null) return null;
        
        var res = new Assignment()
        {
            Id = entity.Id,
            Name = entity.Name,
            DeclarationId = entity.DeclarationId,
        };
        
        return res;
    }

    public AssignmentBLLDto? Map(Assignment? entity)
    {
        if (entity == null) return null;
        
        var res = new AssignmentBLLDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            DeclarationId = entity.DeclarationId,
        };

        return res;
    }
    
    public AssignmentBLLDto Map(AssignmentCreate entity)
    {
        var res = new AssignmentBLLDto()
        {
            Id = Guid.NewGuid(),
            Name = entity.Name,
            DeclarationId = entity.DeclarationId,
        };
        
        return res;
    }
}