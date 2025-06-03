using App.Domain;
using App.Domain.Identity;
using Base.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;


public class AppDbContext : 
    IdentityDbContext<
        AppUser,
        AppRole,
        Guid,
        IdentityUserClaim<Guid>,
        AppUserRole,
        IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>,
        IdentityUserToken<Guid>
    >
{
    public DbSet<AppUser> AppUsers { get; set; } = default!;
    public DbSet<AppRole> AppRoles { get; set; } = default!;
    public DbSet<Declaration> Declarations { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Timelog> Timelogs { get; set; }
    public DbSet<Attachment> Attachments { get; set; } 
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<StudySession> StudySessions { get; set; }
    public DbSet<StudyGroup> StudyGroups { get; set; }
    public DbSet<AppRefreshToken> RefreshTokens { get; set; } = default!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // remove cascade delete
        foreach (var relationship in builder.Model
                     .GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        
        // We have custom UserRole - with separate PK and navigation for Role and User
        // override default Identity EF config
        // builder.Entity<AppUserRole>().HasKey(a => a.Id);
        builder.Entity<AppUserRole>().HasKey(a => new {a.UserId, a.RoleId });
        builder.Entity<AppUserRole>().HasAlternateKey(a => a.Id);


        builder.Entity<AppUserRole>().HasIndex(a => new { a.UserId, a.RoleId }).IsUnique();

        builder.Entity<AppUserRole>()
            .HasOne(a => a.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(a => a.UserId);

        builder.Entity<AppUserRole>()
            .HasOne(a => a.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(a => a.RoleId);

    }
    
    // my old working solution
    // public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    // {
    //     foreach (var entity in ChangeTracker.Entries().Where(e => e.State != EntityState.Deleted))
    //     {
    //         foreach (var prop in entity.Properties)
    //         {
    //             if (prop.CurrentValue is DateTime dateTimeValue)
    //             {
    //                 // todo: find all datetime props, change to utc.
    //                 if (dateTimeValue.Kind == DateTimeKind.Unspecified)
    //                 {
    //                     prop.CurrentValue = DateTime.SpecifyKind(dateTimeValue, DateTimeKind.Utc);
    //                     prop.CurrentValue = dateTimeValue.ToUniversalTime();
    //                 }
    //             }
    //         }
    //     }
    //     
    //     return base.SaveChangesAsync(cancellationToken);
    // }
    
    // Kaver lecture solution
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var addedEntries = ChangeTracker.Entries()
            .Where(e => e is { Entity: IDomainMeta });
        
        foreach (var entry in addedEntries)
        {
            foreach (var prop in entry.Properties)
            {
                if (prop.CurrentValue is DateTime dateTimeValue)
                {
                    if (dateTimeValue.Kind == DateTimeKind.Unspecified)
                    {
                        prop.CurrentValue = DateTime.SpecifyKind(dateTimeValue, DateTimeKind.Utc);
                        prop.CurrentValue = dateTimeValue.ToUniversalTime();
                    }
                }
            }
            
            if (entry.State == EntityState.Added)
            {
                (entry.Entity as IDomainMeta)!.CreatedAt = DateTime.UtcNow;
                (entry.Entity as IDomainMeta)!.CreatedBy = "system";
            } else if (entry.State == EntityState.Modified)
            {
                (entry.Entity as IDomainMeta)!.ModifiedAt = DateTime.UtcNow;
                (entry.Entity as IDomainMeta)!.ModifiedBy = "system";
                
                entry.Property(nameof(IDomainMeta.CreatedBy)).IsModified = false;
                entry.Property(nameof(IDomainMeta.CreatedAt)).IsModified = false;

                if (entry.Entity is IDomainUserId userId)
                {
                    entry.Property(nameof(IDomainUserId.UserId)).IsModified = false;
                }
            }
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }
}