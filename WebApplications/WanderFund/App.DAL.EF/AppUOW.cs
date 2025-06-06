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
    private IAssignmentRepository? _assignmentRepository;
    public IAssignmentRepository AssignmentRepository =>
        _assignmentRepository ??= new AssignmentRepository(UOWDbContext);
    
    private IAttachmentRepository? _attachmentRepository;
    public IAttachmentRepository AttachmentRepository =>
        _attachmentRepository ??= new AttachmentRepository(UOWDbContext);
    
    private ICourseRepository? _courseRepository;
    public ICourseRepository CourseRepository =>
        _courseRepository ??= new CourseRepository(UOWDbContext);
    
    private IDeclarationRepository? _declarationRepository;
    public IDeclarationRepository DeclarationRepository =>
        _declarationRepository ??= new DeclarationRepository(UOWDbContext);
    
    private IRoomRepository? _roomRepository;
    public IRoomRepository RoomRepository =>
        _roomRepository ??= new RoomRepository(UOWDbContext);
    
    private IStudyGroupRepository? _studyGroupRepository;
    public IStudyGroupRepository StudyGroupRepository =>
        _studyGroupRepository ??= new StudyGroupRepository(UOWDbContext);
    
    private IStudySessionRepository? _studySessionRepository;
    public IStudySessionRepository StudySessionRepository =>
        _studySessionRepository ??= new StudySessionRepository(UOWDbContext);
    
    private ITimelogRepository? _timelogRepository;
    public ITimelogRepository TimelogRepository =>
        _timelogRepository ??= new TimelogRepository(UOWDbContext);
    
    // WanderFund
    private ITripRepository? _tripRepository;
    public ITripRepository TripRepository =>
        _tripRepository ??= new TripRepository(UOWDbContext);

    private ITripExpenseRepository? _tripExpenseRepository;
    public ITripExpenseRepository TripExpenseRepository =>
        _tripExpenseRepository ??= new TripExpenseRepository(UOWDbContext);

    private IUserTripRepository? _userTripRepository;
    public IUserTripRepository UserTripRepository =>
        _userTripRepository ??= new UserTripRepository(UOWDbContext);
    
    private IExpenseRepository? _expenseRepository;
    public IExpenseRepository ExpenseRepository =>
        _expenseRepository ??= new ExpenseRepository(UOWDbContext);
    
    private IExpenseCategoryRepository? _expenseCategoryRepository;
    public IExpenseCategoryRepository ExpenseCategoryRepository =>
        _expenseCategoryRepository ??= new ExpenseCategoryRepository(UOWDbContext);
}