using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.DAL.DTO;

public class DeclarationDto : IDomainId
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Active), Prompt = nameof(Active), ResourceType = typeof(App.Resources.Domain.Declaration))]
    public Boolean Active { get; set; }
    
    public Guid CourseId { get; set; }
    [Display(Name = nameof(Course), Prompt = nameof(Course), ResourceType = typeof(App.Resources.Domain.Declaration))]
    public CourseDto? Course { get; set; }

    [Display(Name = nameof(Timelogs), Prompt = nameof(Timelogs), ResourceType = typeof(App.Resources.Domain.Declaration))]
    public ICollection<TimelogDto>? Timelogs { get; set; }
    [Display(Name = nameof(Assignments), Prompt = nameof(Assignments), ResourceType = typeof(App.Resources.Domain.Declaration))]
    public ICollection<AssignmentDto>? Assignments { get; set; }
}