using App.BLL.Interfaces;
using App.BLL.Mappers;
using App.BLL.Services;
using App.DAL.EF;
using App.DAL.Interfaces;
using Base.BLL;

namespace App.BLL;

public class AppBLL : BaseBLL<IAppUOW>, IAppBLL
{
    public AppBLL(IAppUOW uow) : base(uow)
    {
    }

    private IAssignmentService? _assignmentService;
    public IAssignmentService AssignmentService =>
        _assignmentService ??= new AssignmentService(
            BLLUOW,
            new AssignmentBLLMapper());
    
    private IAttachmentService? _attachmentService;
    public IAttachmentService AttachmentService =>
        _attachmentService ??= new AttachmentService(
            BLLUOW,
            new AttachmentBLLMapper());
    
    private ICourseService? _courseService;
    public ICourseService CourseService =>
        _courseService ??= new CourseService(
            BLLUOW,
            new CourseBLLMapper());
    
    private IDeclarationService? _declarationService;
    public IDeclarationService DeclarationService =>
        _declarationService ??= new DeclarationService(
            BLLUOW,
            new DeclarationBLLMapper());
    
    private IRoomService? _roomService;
    public IRoomService RoomService =>
        _roomService ??= new RoomService(
            BLLUOW,
            new RoomBLLMapper());
    
    private IStudyGroupService? _studyGroupService;
    public IStudyGroupService StudyGroupService =>
        _studyGroupService ??= new StudyGroupService(
            BLLUOW,
            new StudyGroupBLLMapper());
    
    private IStudySessionService? _studySessionService;
    public IStudySessionService StudySessionService =>
        _studySessionService ??= new StudySessionService(
            BLLUOW,
            new StudySessionBLLMapper());
    
    private ITimelogService? _timelogService;
    public ITimelogService TimelogService =>
        _timelogService ??= new TimelogService(
            BLLUOW,
            new TimelogBLLMapper());
}