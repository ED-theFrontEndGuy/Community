using App.DAL.DTO;
using App.Domain;
using Base.Interfaces;

namespace App.DAL.EF.Mappers;

public class ExpenseCategoryUOWMapper : IMapper<ExpenseCategoryDto, ExpenseCategory>
{
    public ExpenseCategoryDto? Map(ExpenseCategory? entity)
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

    public ExpenseCategory? Map(ExpenseCategoryDto? entity)
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
}