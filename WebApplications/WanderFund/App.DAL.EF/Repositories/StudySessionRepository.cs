using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class StudySessionRepository : BaseRepository<StudySessionDto, StudySession>, IStudySessionRepository
{
    public StudySessionRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new StudySessionUOWMapper())
    {
    }

    public override async Task<IEnumerable<StudySessionDto>> AllAsync(Guid userId = default)
    {
        return (await RepositoryDbSet
            .Include(s => s.Assignment)
            .Include(s => s.Room)
            .ToListAsync())
            .Select(e => Mapper.Map(e)!);
    }

    public override async Task<StudySessionDto?> FindAsync(Guid id, Guid userId = default)
    {
        return Mapper.Map(await RepositoryDbSet
            .Include(s => s.Assignment)
            .Include(s => s.Room)
            .Where(s => s.Id == id)
            .FirstOrDefaultAsync());
    }
}