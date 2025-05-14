using App.DAL.DTO;
using Base.DAL.Interfaces;
using App.Domain;

namespace App.DAL.EF.Mappers;

public class AssignmentMapper : IMapper<AssignmentDto, Assignment>
{
    public AssignmentDto? Map(Assignment? entity)
    {
        throw new NotImplementedException();
    }

    public Assignment? Map(AssignmentDto? entity)
    {
        throw new NotImplementedException();
    }
}