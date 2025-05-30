using App.BLL.DTO;
using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class StudyGroupMapper : IMapper<StudyGroup, StudyGroupBLLDto>
{
    public StudyGroup? Map(StudyGroupBLLDto? entity)
    {
        if (entity == null) return null;

        var res = new StudyGroup()
        {
            Id = entity.Id,
            Name = entity.Name!,
            StudySessionId = entity.StudySessionId
        };

        return res;
    }

    public StudyGroupBLLDto? Map(StudyGroup? entity)
    {
        if (entity == null) return null;
        
        var res = new StudyGroupBLLDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            StudySessionId = entity.StudySessionId,
        };

        return res;
    }
    
    public StudyGroupBLLDto Map(StudyGroupCreate entity)
    {
        var res = new StudyGroupBLLDto()
        {
            Id = Guid.NewGuid(),
            Name = entity.Name,
            StudySessionId = entity.StudySessionId,
        };

        return res;
    }
}