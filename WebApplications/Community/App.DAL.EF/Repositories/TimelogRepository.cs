using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TimelogRepository : BaseRepository<TimelogDto, Timelog>, ITimelogRepository
{
    public TimelogRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new TimelogUOWMapper())
    {
    }
    
    public override async Task<IEnumerable<TimelogDto>> AllAsync(Guid userId = default)
    {
        return (await RepositoryDbSet
            .Include(t => t.Declaration)
            .ThenInclude(d => d!.Course)
            .Include(t => t.Assignment)
            .Where(t => t.Declaration!.UserId == userId)
            .ToListAsync())
            .Select(e => Mapper.Map(e)!);
    }

    public override async Task<TimelogDto?> FindAsync(Guid id, Guid userId = default)
    {
        return Mapper.Map(await RepositoryDbSet
            .Include(t => t.Declaration)
            .ThenInclude(d => d!.Course)
            .Include(t => t.Assignment)
            .Where(t => t.Id == id && t.Declaration!.UserId == userId)
            .FirstOrDefaultAsync());
    }
}