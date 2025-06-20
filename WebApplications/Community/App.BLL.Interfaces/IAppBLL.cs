﻿using Base.BLL.Interfaces;

namespace App.BLL.Interfaces;

public interface IAppBLL : IBaseBLL
{
    IAssignmentService AssignmentService { get; }
    IAttachmentService AttachmentService { get; }
    ICourseService CourseService { get; }
    IDeclarationService DeclarationService { get; }
    IRoomService RoomService { get; }
    IStudyGroupService StudyGroupService { get; }
    // IStudyGroupUserService StudyGroupUserService { get; }
    IStudySessionService StudySessionService { get; }
    ITimelogService TimelogService { get; }
}