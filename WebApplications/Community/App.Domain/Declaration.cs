using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Declaration : BaseEntityUser<AppUser>
{
    [Display(Name = nameof(Active), Prompt = nameof(Active), ResourceType = typeof(App.Resources.Domain.Declaration))]
    public Boolean Active { get; set; }
    
    public Guid CourseId { get; set; }
    [Display(Name = nameof(Course), Prompt = nameof(Course), ResourceType = typeof(App.Resources.Domain.Declaration))]
    public Course? Course { get; set; }

    [Display(Name = nameof(Timelogs), Prompt = nameof(Timelogs), ResourceType = typeof(App.Resources.Domain.Declaration))]
    public ICollection<Timelog>? Timelogs { get; set; }
    [Display(Name = nameof(Assignments), Prompt = nameof(Assignments), ResourceType = typeof(App.Resources.Domain.Declaration))]
    public ICollection<Assignment>? Assignments { get; set; }
}