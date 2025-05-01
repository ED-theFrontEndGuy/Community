using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface IAppUOW : IBaseUOW
{
    IAchievementRepository AchievementRepository { get; }
    IAssignmentRepository AssignmentRepository { get; }
    IAttachmentRepository AttachmentRepository { get; }
    IConversationRepository ConversationRepository { get; }
    ICourseRepository CourseRepository { get; }
    IDashboardRepository DashboardRepository { get; }
    IDeclarationRepository DeclarationRepository { get; }
    IMessageRepository MessageRepository { get; }
    IRoomRepository RoomRepository { get; }
    IStudyGroupRepository StudyGroupRepository { get; }
    ITimelogRepository TimelogRepository { get; }
    IUserAchievementRepository UserAchievementRepository { get; }
}