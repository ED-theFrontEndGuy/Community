using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class TripExpenseRepository : BaseRepository<TripExpenseDto, TripExpense>, ITripExpenseRepository
{
    public TripExpenseRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new TripExpenseUOWMapper())
    {
    }
}