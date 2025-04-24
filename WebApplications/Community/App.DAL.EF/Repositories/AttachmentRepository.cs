using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class AttachmentRepository : BaseRepository<Attachment>, IAttachmentRepository
{
    public AttachmentRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }

    public override async Task<IEnumerable<Attachment>> AllAsync(Guid userid = default)
    {
        return await RepositoryDbSet
            .Include(a => a.Assignment)
            .ToListAsync();
    }

    public override async Task<Attachment?> FindAsync(Guid id, Guid userid = default)
    {
        return await RepositoryDbSet
            .Include(a => a.Assignment)
            .FirstOrDefaultAsync();
    }
}