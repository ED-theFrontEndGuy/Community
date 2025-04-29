using App.DAL.EF.Repositories;
using App.DAL.Interfaces;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUOW : BaseUOW<AppDbContext>, IAppUOW
{
    public AppUOW(AppDbContext uowDbContext) : base(uowDbContext)
    {
    }
    
    // simple caching
    private IAchievementRepository? _achievementRepository;
    public IAchievementRepository AchievementRepository =>
        _achievementRepository ??= new AchievementRepository(UOWDbContext);

    private IAssignmentRepository? _assignmentRepository;
    public IAssignmentRepository AssignmentRepository =>
        _assignmentRepository ??= new AssignmentRepository(UOWDbContext);
    
    // ToDo add the rest repos
}