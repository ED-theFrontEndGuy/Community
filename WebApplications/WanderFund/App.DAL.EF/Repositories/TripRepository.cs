using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TripRepository : BaseRepository<TripDto, Trip>, ITripRepository
{
    public TripRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new TripUOWMapper())
    {
    }
    
    public override async Task<IEnumerable<TripDto>> AllAsync(Guid userId = default)
    {
        return (await RepositoryDbSet
                .Include(t => t.TripExpenses)!
                .ThenInclude(t => t.Expense)
                .ToListAsync())
                .Select(e => Mapper.Map(e)!);
    }
    
    public override async Task<TripDto?> FindAsync(Guid id, Guid userId = default)
    {
        return Mapper.Map(await RepositoryDbSet
            .Include(t => t.TripExpenses)!
            .ThenInclude(t => t.Expense)
            .Include(t => t.UserTrips)!
            .ThenInclude(ut => ut.User)
            .Where(u => u.Id == id && u.UserTrips!.Any(ut => ut.UserId == userId))
            .FirstOrDefaultAsync());
    }
}