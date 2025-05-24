using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class AssignmentRepository : BaseRepository<AssignmentDto, Assignment>, IAssignmentRepository
{
    public AssignmentRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new AssignmentUOWMapper())
    {
    }

    public override async Task<IEnumerable<AssignmentDto>> AllAsync(Guid userId = default)
    {
        return (await RepositoryDbSet
            .Include(a => a.Declaration)
                .ThenInclude(d => d!.Course)
            .Where(a => a.Declaration!.UserId == userId)
            .ToListAsync())
            .Select(e => UOWMapper.Map(e)!);
    }

    public override async Task<AssignmentDto?> FindAsync(Guid id, Guid userId = default)
    {
        return UOWMapper.Map(await RepositoryDbSet
            .Include(a => a.Declaration)
                .ThenInclude(d => d!.Course)
            .Where(a => a.Id == id && a.Declaration!.UserId == userId)
            .FirstOrDefaultAsync());
    }
}