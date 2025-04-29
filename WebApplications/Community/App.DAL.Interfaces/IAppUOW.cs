using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface IAppUOW : IBaseUOW
{
    IAchievementRepository AchievementRepository { get; }
    IAssignmentRepository AssignmentRepository { get; }
    // TODO add the rest repositories here
}