using App.DAL.DTO;
using Base.DAL.Interfaces;
using App.Domain;

namespace App.DAL.EF.Mappers;

public class AssignmentMapper : IMapper<AssignmentDto, Assignment>
{
    public AssignmentDto? Map(Assignment? entity)
    {
        if (entity == null) return null;
        
        var res = new AssignmentDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            DeclarationId = entity.DeclarationId,
            Declaration = entity.Declaration == null
                ? null
                : new DeclarationDto()
                {
                    Id = entity.Declaration.Id,
                    Active = entity.Declaration.Active,
                    CourseId = entity.Declaration.CourseId,
                    Course = entity.Declaration.Course == null
                        ? null
                        : new CourseDto()
                        {
                            Id = entity.Declaration.Course.Id,
                            Name = entity.Declaration.Course.Name,
                        }
                }
        };
        
        return res;
    }

    public Assignment? Map(AssignmentDto? entity)
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
}