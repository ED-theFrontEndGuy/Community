Migrations
~~~sh
dotnet ef migrations add InitialCreate

dotnet ef migrations add --project App.DAL.EF --startup-project WebApp --context AppDbContext InitialCreate

dotnet ef database update --project App.DAL.EF --startup-project WebApp --context AppDbContext AddDashboardTable

dotnet ef database update --project App.DAL.EF --startup-project WebApp --context AppDbContext

dotnet ef migrations remove --project App.DAL.EF --startup-project WebApp --context AppDbContext

dotnet ef database --project App.DAL.EF --startup-project WebApp update
dotnet ef database --project App.DAL.EF --startup-project WebApp drop
~~~

MVC Controllers
~~~sh
cd WebApp

dotnet aspnet-codegenerator controller -name PersonsController -actions -m App.Domain.Person -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ContactTypesController -actions -m App.Domain.ContactType -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name ContactsController -actions -m App.Domain.Contact -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator controller -name UsersController -actions -m App.Domain.User -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
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

ApiControllers
~~~sh
dotnet aspnet-codegenerator controller -name PersonsController -m App.Domain.Person -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ContactTypesController -m App.Domain.ContactType -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name ContactsController -m App.Domain.Contact -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
~~~

~~~docker-compose.yml
services:
  postgres:
    container_name: "postgres"
    # https://github.com/baosystems/docker-postgis/pkgs/container/postgis
    image: ghcr.io/baosystems/postgis:16
    command: postgres -c 'max_connections=1000'
    restart: unless-stopped
    environment:
      - POSTGRES_USER=postgres
      - POSTGRES_PASSWORD=postgres
    ports:
      - "5432:5432"
    volumes:
      - postgres-data:/var/lib/postgresql/data
      - ./pg-dump:/var/lib/postgresql/dump

volumes: 
    postgres-data:

networks:
  default:
    name: infra
~~~