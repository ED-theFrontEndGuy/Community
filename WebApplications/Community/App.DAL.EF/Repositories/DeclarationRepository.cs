using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class DeclarationRepository : BaseRepository<Declaration>, IDeclarationRepository
{
    public DeclarationRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }

    public override async Task<IEnumerable<Declaration>> AllAsync(Guid userId = default)
    {
        return await RepositoryDbSet
            .Include(ua => ua.Course)
            .Include(ua => ua.User)
            .Where(ua => ua.UserId == userId)
            .ToListAsync();
    }
    
    public override async Task<Declaration?> FindAsync(Guid id, Guid userId = default)
    {
        return await RepositoryDbSet
            .Include(d => d.Course)
            .Include(d => d.User)
            .Where(d => d.Id == id && d.UserId == userId)
            .FirstOrDefaultAsync();
    }
}