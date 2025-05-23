using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class AttachmentRepository : BaseRepository<AttachmentDto, Attachment>, IAttachmentRepository
{
    public AttachmentRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new AttachmentUOWMapper())
    {
    }

    public override async Task<IEnumerable<AttachmentDto>> AllAsync(Guid userid = default)
    {
        return (await RepositoryDbSet
            .Include(a => a.Assignment)
            .ToListAsync())
            .Select(e => UOWMapper.Map(e)!);
    }

    public override async Task<AttachmentDto?> FindAsync(Guid id, Guid userid = default)
    {
        return UOWMapper.Map(await RepositoryDbSet
            .Include(a => a.Assignment)
            .FirstOrDefaultAsync());
    }
}