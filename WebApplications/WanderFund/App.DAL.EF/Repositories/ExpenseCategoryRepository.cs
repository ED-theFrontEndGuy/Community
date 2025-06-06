using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ExpenseCategoryRepository : BaseRepository<ExpenseCategoryDto, ExpenseCategory>, IExpenseCategoryRepository
{
    public ExpenseCategoryRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new ExpenseCategoryUOWMapper())
    {
    }
}
