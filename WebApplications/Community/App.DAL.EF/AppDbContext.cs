using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<AppUser> AppUsers { get; set; } = default!;
    public DbSet<AppRole> AppRoles { get; set; } = default!;
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<UserAchievement> UserAchievements { get; set; }
    public DbSet<Dashboard> Dashboards { get; set; }
    public DbSet<Declaration> Declarations { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Timelog> Timelogs { get; set; }
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<StudySession> StudySessions { get; set; }
    public DbSet<StudyGroup> StudyGroups { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<Message> Messages { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entity in ChangeTracker.Entries().Where(e => e.State != EntityState.Deleted))
        {
            foreach (var prop in entity.Properties)
            {
                if (prop.CurrentValue is DateTime dateTimeValue)
                {
                    // todo: find all datetime props, change to utc.
                    if (dateTimeValue.Kind == DateTimeKind.Unspecified)
                    {
                        prop.CurrentValue = DateTime.SpecifyKind(dateTimeValue, DateTimeKind.Utc);
                        prop.CurrentValue = dateTimeValue.ToUniversalTime();
                    }
                }
            }
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }
}