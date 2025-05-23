using App.BLL.Interfaces;
using App.BLL.Mappers;
using App.BLL.Services;
using App.DAL.EF;
using Base.BLL;

namespace App.BLL;

public class AppBLL : BaseBLL<AppUOW>, IAppBLL
{
    public AppBLL(AppUOW uow) : base(uow)
    {
    }

    private IAssignmentService? _assignmentService;
    public IAssignmentService AssignmentService =>
        _assignmentService ??= new AssignmentService(
            BLLUOW,
            BLLUOW.AssignmentRepository, 
            new AssignmentBLLMapper());
    
    private IAttachmentService? _attachmentService;
    public IAttachmentService AttachmentService =>
        _attachmentService ??= new AttachmentService(
            BLLUOW,
            BLLUOW.AttachmentRepository,
            new AttachmentBLLMapper());
    
    private ICourseService? _courseService;
    public ICourseService CourseService =>
        _courseService ??= new CourseService(
            BLLUOW,
            BLLUOW.CourseRepository,
            new CourseBLLMapper());
    
    private IDeclarationService? _declarationService;
    public IDeclarationService DeclarationService =>
        _declarationService ??= new DeclarationService(
            BLLUOW,
            BLLUOW.DeclarationRepository,
            new DeclarationBLLMapper());
    
    private IRoomService? _roomService;
    public IRoomService RoomService =>
        _roomService ??= new RoomService(
            BLLUOW,
            BLLUOW.RoomRepository,
            new RoomBLLMapper());
    
    private IStudyGroupService? _studyGroupService;
    public IStudyGroupService StudyGroupService =>
        _studyGroupService ??= new StudyGroupService(
            BLLUOW,
            BLLUOW.StudyGroupRepository,
            new StudyGroupBLLMapper());
    
    private IStudySessionService? _studySessionService;
    public IStudySessionService StudySessionService =>
        _studySessionService ??= new StudySessionService(
            BLLUOW,
            BLLUOW.StudySessionRepository,
            new StudySessionBLLMapper());
    
    private ITimelogService? _timelogService;
    public ITimelogService TimelogService =>
        _timelogService ??= new TimelogService(
            BLLUOW,
            BLLUOW.TimelogRepository,
            new TimelogBLLMapper());
}