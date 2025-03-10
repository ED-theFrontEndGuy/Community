using App.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext
{
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<UserAchievement> UserAchievements { get; set; }
    public DbSet<Dashboard> Dashboards { get; set; }
    public DbSet<Declaration> Declarations { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Timelog> Timelogs { get; set; }
    public DbSet<Bookmark> Bookmarks { get; set; }
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
}