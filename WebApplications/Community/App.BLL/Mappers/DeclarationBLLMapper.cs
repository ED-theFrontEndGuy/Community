using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Interfaces;

namespace App.BLL.Mappers;

public class DeclarationBLLMapper : IBLLMapper<DeclarationBLLDto, DeclarationDto>
{
    public DeclarationBLLDto? Map(DeclarationDto? entity)
    {
        if (entity == null) return null;
        
        var res = new DeclarationBLLDto()
        {
            Id = entity.Id,
            Active = entity.Active,
            CourseId = entity.CourseId,
            Course = entity.Course == null
                ? null
                : new CourseBLLDto()
                {
                    Id = entity.Course.Id,
                    Name = entity.Course.Name,
                }
        };

        return res;
    }

    public DeclarationDto? Map(DeclarationBLLDto? entity)
    {
        if (entity == null) return null;
        
        var res = new DeclarationDto()
        {
            Id = entity.Id,
            Active = entity.Active,
            CourseId = entity.CourseId,
        };
        
        return res;
    }
}