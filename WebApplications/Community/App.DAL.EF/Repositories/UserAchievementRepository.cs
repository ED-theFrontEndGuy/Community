using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserAchievementRepository : BaseRepository<UserAchievement>, IUserAchievementRepository
{
    public UserAchievementRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
    
    public override async Task<IEnumerable<UserAchievement>> AllAsync(Guid userId = default)
    {
        return await RepositoryDbSet
            .Include(ua => ua.Achievement)
            .Include(ua => ua.User)
            .Where(ua => ua.UserId == userId)
            .ToListAsync();
    }

    public override async Task<UserAchievement?> FindAsync(Guid id, Guid userId = default)
    {
        return await RepositoryDbSet
            .Include(ua => ua.Achievement)
            .Include(ua => ua.User)
            .Where(ua => ua.Id == id && ua.UserId == userId)
            .FirstOrDefaultAsync();
    }
}