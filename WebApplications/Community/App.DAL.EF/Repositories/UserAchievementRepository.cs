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
            .Include(ua => ua.Achievement)             // Include related Achievement data
            .Include(ua => ua.User)                    // Include related User data
            .Where(ua => ua.UserId == userId)          // Filter by the current user's ID
            .ToListAsync();
    }

}