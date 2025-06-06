using App.DAL.DTO;
using App.Domain;
using Base.Interfaces;

namespace App.DAL.EF.Mappers;

public class DeclarationUOWMapper : IMapper<DeclarationDto, Declaration>
{
    public DeclarationDto? Map(Declaration? entity)
    {
        if (entity == null) return null;
        
        var res = new DeclarationDto()
        {
            Id = entity.Id,
            Active = entity.Active,
            CourseId = entity.CourseId,
            Course = entity.Course == null
                ? null
                : new CourseDto()
                {
                    Id = entity.Course.Id,
                    Name = entity.Course.Name,
                }
        };

        return res;
    }

    public Declaration? Map(DeclarationDto? entity)
    {
        if (entity == null) return null;
        
        var res = new Declaration()
        {
            Id = entity.Id,
            Active = entity.Active,
            CourseId = entity.CourseId,
        };
        
        return res;
    }
}