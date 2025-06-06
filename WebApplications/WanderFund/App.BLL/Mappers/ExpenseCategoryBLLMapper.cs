using App.BLL.DTO;
using App.DAL.DTO;
using Base.Interfaces;

namespace App.BLL.Mappers;

public class ExpenseCategoryBLLMapper : IMapper<ExpenseCategoryBLLDto, ExpenseCategoryDto>
{
    public ExpenseCategoryBLLDto? Map(ExpenseCategoryDto? entity)
    {
        if (entity == null) return null;
        
        var res = new ExpenseCategoryBLLDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
        };
        
        return res;
    }

    public ExpenseCategoryDto? Map(ExpenseCategoryBLLDto? entity)
    {
        if (entity == null) return null;
        
        var res = new ExpenseCategoryDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
        };
        
        return res;
    }
}