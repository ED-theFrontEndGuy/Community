using App.DAL.DTO;
using App.Domain;
using Base.DAL.Interfaces;

namespace App.DAL.EF.Mappers;

public class DeclarationMapper : IMapper<DeclarationDto, Declaration>
{
    public DeclarationDto? Map(Declaration? entity)
    {
        throw new NotImplementedException();
    }

    public Declaration? Map(DeclarationDto? entity)
    {
        throw new NotImplementedException();
    }
}