### Commands

Migrations
~~~sh
dotnet ef migrations add InitialCreate
dotnet ef migrations add --project App.DAL.EF --startup-project WebApp --context AppDbContext InitialCreate
dotnet ef database update --project App.DAL.EF --startup-project WebApp --context AppDbContext InitialCreate

dotnet ef migrations add --project App.DAL.EF --startup-project WebApp --context AppDbContext MassiveCleanup
dotnet ef database update --project App.DAL.EF --startup-project WebApp --context AppDbContext MassiveCleanup

dotnet ef migrations remove --project App.DAL.EF --startup-project WebApp --context AppDbContext

fyi
dotnet ef database --project App.DAL.EF --startup-project WebApp update
dotnet ef database --project App.DAL.EF --startup-project WebApp drop
~~~

MVC Controllers
~~~sh
cd WebApp

dotnet aspnet-codegenerator controller -name AppUsersController -actions -m App.Domain.Identity.AppUser -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name AchievementsController -actions -m App.Domain.Achievement -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UserAchievementsController -actions -m App.Domain.UserAchievement -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name DashboardsController -actions -m App.Domain.Dashboard -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name DeclarationsController -actions -m App.Domain.Declaration -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name CoursesController -actions -m App.Domain.Course -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TimelogsController -actions -m App.Domain.Timelog -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name AttachmentsController -actions -m App.Domain.Attachment -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name AssignmentsController -actions -m App.Domain.Assignment -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RoomsController -actions -m App.Domain.Room -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ConversationsController -actions -m App.Domain.Conversation -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MessagesController -actions -m App.Domain.Message -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name StudySessionsController -actions -m App.Domain.StudySession -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name StudyGroupsController -actions -m App.Domain.StudyGroup -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~

extend appuser
~~~sh
dotnet aspnet-codegenerator identity -dc App.DAL.EF.AppDbContext -f
~~~

Admin Controllers
~~~sh
cd WebApp

dotnet aspnet-codegenerator controller -name RefreshTokensController        -actions -m  App.Domain.Identity.AppRefreshToken        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UsersController        -actions -m  App.Domain.Identity.AppUser        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name RolesController        -actions -m  App.Domain.Identity.AppRole        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UserRolesController        -actions -m  App.Domain.Identity.AppUserRole        -dc AppDbContext -outDir Areas/Admin/Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
~~~

ApiControllers
~~~sh
dotnet aspnet-codegenerator controller -name CoursesController -m App.Domain.Course -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name AssignmentsController -m App.Domain.Assignment -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name AttachmentsController -m App.Domain.Attachment -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name DeclarationsController -m App.Domain.Declaration -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name RoomsController -m App.Domain.Room -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name StudyGroupsController -m App.Domain.StudyGroup -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name StudySessionsController -m App.Domain.StudySession -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name TimelogsController -m App.Domain.Timelog -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
~~~

~~~sh
docker compose --project-name local-dev-infra --file docker-compose.yml up --build --remove-orphans --detach
~~~