using Base.DAL.Interfaces;

namespace App.DAL.Interfaces;

public interface IAppUOW : IBaseUOW
{
    IAssignmentRepository AssignmentRepository { get; }
    IAttachmentRepository AttachmentRepository { get; }
    ICourseRepository CourseRepository { get; }
    IDeclarationRepository DeclarationRepository { get; }
    IRoomRepository RoomRepository { get; }
    IStudyGroupRepository StudyGroupRepository { get; }
    IStudySessionRepository StudySessionRepository { get; }
    ITimelogRepository TimelogRepository { get; }
}