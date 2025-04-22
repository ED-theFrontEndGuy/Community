using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class StudySessionRepository : BaseRepository<StudySession>, IStudySessionRepository
{
    public StudySessionRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }

    public override async Task<IEnumerable<StudySession>> AllAsync(Guid userId = default)
    {
        return await RepositoryDbSet
            .Include(s => s.Assignment)
            .Include(s => s.Room)
            .ToListAsync();
    }

    public override async Task<StudySession?> FindAsync(Guid id, Guid userId = default)
    {
        return await RepositoryDbSet
            .Include(s => s.Assignment)
            .Include(s => s.Room)
            .Where(s => s.Id == id)
            .FirstOrDefaultAsync();
    }
}