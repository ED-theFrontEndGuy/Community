using App.DAL.Interfaces;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class DashboardRepository : BaseRepository<Dashboard>, IDashboardRepository
{
    public DashboardRepository(AppDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}