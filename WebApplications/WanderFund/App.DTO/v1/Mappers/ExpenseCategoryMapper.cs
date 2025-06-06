using App.BLL.DTO;
using Base.Interfaces;

namespace App.DTO.v1.Mappers;

public class ExpenseCategoryMapper : IMapper<ExpenseCategory, ExpenseCategoryBLLDto>
{
    public ExpenseCategory? Map(ExpenseCategoryBLLDto? entity)
    {
        if (entity == null) return null;

        var res = new ExpenseCategory()
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
        };

        return res;
    }

    public ExpenseCategoryBLLDto? Map(ExpenseCategory? entity)
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
    
    public ExpenseCategoryBLLDto Map(ExpenseCategoryCreate entity)
    {
        var res = new ExpenseCategoryBLLDto()
        {
            Id = Guid.NewGuid(),
            Name = entity.Name,
            Description = entity.Description,
        };

        return res;
    }
}