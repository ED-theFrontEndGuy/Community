using App.DAL.DTO;
using App.DAL.EF.Mappers;
using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class StudyGroupRepository : BaseRepository<StudyGroupDto, StudyGroup>, IStudyGroupRepository
{
    public StudyGroupRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext, new StudyGroupMapper())
    {
    }
    
    public override async Task<IEnumerable<StudyGroupDto>> AllAsync(Guid userId = default)
    {
        return (await RepositoryDbSet
            .Include(s => s.StudySession)
            .Include(s => s.StudyGroupUsers)
            .ThenInclude(sgu => sgu.User)
            .Where(s => s.StudyGroupUsers!.Any(sgu => sgu.UserId == userId))
            .ToListAsync())
            .Select(e => Mapper.Map(e)!);
    }

    public override async Task<StudyGroupDto?> FindAsync(Guid id, Guid userId = default)
    {
        return Mapper.Map(await RepositoryDbSet
            .Include(s => s.StudySession)
            .Include(s => s.StudyGroupUsers)
            .ThenInclude(sgu => sgu.User)
            .Where(s => s.Id == id && s.StudyGroupUsers!.Any(sgu => sgu.UserId == userId))
            .FirstOrDefaultAsync());
    }
}