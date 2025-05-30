using App.BLL.DTO;
using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class DeclarationMapper : IMapper<Declaration, DeclarationBLLDto>
{
    public Declaration? Map(DeclarationBLLDto? entity)
    {
        if (entity == null) return null;
        
        var res = new Declaration()
        {
            Id = entity.Id,
            Active = entity.Active,
            CourseId = entity.CourseId,
            CourseName = (entity.Course == null
                ? null
                : entity.Course.Name)!
        };

        return res;
    }

    public DeclarationBLLDto? Map(Declaration? entity)
    {
        if (entity == null) return null;
        
        var res = new DeclarationBLLDto()
        {
            Id = entity.Id,
            Active = entity.Active,
            CourseId = entity.CourseId,
        };
        
        return res;
    }
    
    public DeclarationBLLDto Map(DeclarationCreate entity)
    {
        var res = new DeclarationBLLDto()
        {
            Id = Guid.NewGuid(),
            Active = entity.Active,
            CourseId = entity.CourseId,
        };
        
        return res;
    }
}