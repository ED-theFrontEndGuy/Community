using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TimelogRepository : BaseRepository<Timelog>, ITimelogRepository
{
    public TimelogRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
    
    public override async Task<IEnumerable<Timelog>> AllAsync(Guid userId = default)
    {
        return await RepositoryDbSet
            .Include(t => t.Declaration)
            .ThenInclude(d => d!.Course)
            .Include(t => t.Assignment)
            .Where(t => t.Declaration!.UserId == userId)
            .ToListAsync();
    }

    public override async Task<Timelog?> FindAsync(Guid id, Guid userId = default)
    {
        return await RepositoryDbSet
            .Include(t => t.Declaration)
            .ThenInclude(d => d!.Course)
            .Include(t => t.Assignment)
            .Where(t => t.Id == id && t.Declaration!.UserId == userId)
            .FirstOrDefaultAsync();
    }
}