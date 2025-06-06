using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class DeclarationRepository : BaseRepository<DeclarationDto, Declaration>, IDeclarationRepository
{
    public DeclarationRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new DeclarationUOWMapper())
    {
    }

    public override async Task<IEnumerable<DeclarationDto>> AllAsync(Guid userId = default)
    {
        return (await RepositoryDbSet
            .Include(ua => ua.Course)
            .Include(ua => ua.User)
            .Where(ua => ua.UserId == userId)
            .ToListAsync())
            .Select(e => Mapper.Map(e)!);
    }
    
    public override async Task<DeclarationDto?> FindAsync(Guid id, Guid userId = default)
    {
        return Mapper.Map(await RepositoryDbSet
            .Include(d => d.Course)
            .Include(d => d.User)
            .Where(d => d.Id == id && d.UserId == userId)
            .FirstOrDefaultAsync());
    }
}