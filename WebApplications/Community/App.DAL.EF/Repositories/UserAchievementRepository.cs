using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class UserAchievementRepository : BaseRepository<UserAchievement>, IUserAchievementRepository
{
    public UserAchievementRepository(DbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}