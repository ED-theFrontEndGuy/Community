using App.BLL.DTO;
using App.DAL.DTO;
using Base.BLL.Interfaces;

namespace App.BLL.Mappers;

public class AssignmentBLLMapper : IBLLMapper<AssignmentBLLDto, AssignmentDto>
{
    public AssignmentBLLDto? Map(AssignmentDto? entity)
    {
        if (entity == null) return null;
        
        var res = new AssignmentBLLDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            DeclarationId = entity.DeclarationId,
            Declaration = entity.Declaration == null
                ? null
                : new DeclarationBLLDto()
                {
                    Id = entity.Declaration.Id,
                    Active = entity.Declaration.Active,
                    CourseId = entity.Declaration.CourseId,
                    Course = entity.Declaration.Course == null
                        ? null
                        : new CourseBLLDto()
                        {
                            Id = entity.Declaration.Course.Id,
                            Name = entity.Declaration.Course.Name,
                        }
                }
        };
        
        return res;
    }

    public AssignmentDto? Map(AssignmentBLLDto? entity)
    {
        if (entity == null) return null;
        
        var res = new AssignmentDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            DeclarationId = entity.DeclarationId,
        };

        return res;
    }
}