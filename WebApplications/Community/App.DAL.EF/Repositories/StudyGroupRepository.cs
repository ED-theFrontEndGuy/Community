using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class StudyGroupRepository : BaseRepository<StudyGroup>, IStudyGroupRepository
{
    public StudyGroupRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
    
    public override async Task<IEnumerable<StudyGroup>> AllAsync(Guid userId = default)
    {
        return await RepositoryDbSet
            .Include(s => s.StudySession)
            .Include(s => s.User)
            .Where(s => s.UserId == userId)
            .ToListAsync();
    }

    public override async Task<StudyGroup?> FindAsync(Guid id, Guid userId = default)
    {
        return await RepositoryDbSet
            .Include(s => s.StudySession)
            .Include(s => s.User)
            .Where(s => s.Id == id && s.UserId == userId)
            .FirstOrDefaultAsync();
    }
}