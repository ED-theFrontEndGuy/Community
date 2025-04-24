using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class AssignmentRepository : BaseRepository<Assignment>, IAssignmentRepository
{
    public AssignmentRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }

    public override async Task<IEnumerable<Assignment>> AllAsync(Guid userId = default)
    {
        return await RepositoryDbSet
            .Include(a => a.Declaration)
                .ThenInclude(d => d!.Course)
            .Where(a => a.Declaration!.UserId == userId)
            .ToListAsync();
    }

    public override async Task<Assignment?> FindAsync(Guid id, Guid userId = default)
    {
        return await RepositoryDbSet
            .Include(a => a.Declaration)
                .ThenInclude(d => d!.Course)
            .Where(a => a.Id == id && a.Declaration!.UserId == userId)
            .FirstOrDefaultAsync();
    }
}