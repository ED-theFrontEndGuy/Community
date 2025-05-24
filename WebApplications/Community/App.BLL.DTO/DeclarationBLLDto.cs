using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.BLL.DTO;

public class DeclarationBLLDto : IDomainId
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(Active), Prompt = nameof(Active), ResourceType = typeof(App.Resources.Domain.Declaration))]
    public Boolean Active { get; set; }
    
    public Guid CourseId { get; set; }
    [Display(Name = nameof(Course), Prompt = nameof(Course), ResourceType = typeof(App.Resources.Domain.Declaration))]
    public CourseBLLDto? Course { get; set; }

    [Display(Name = nameof(Timelogs), Prompt = nameof(Timelogs), ResourceType = typeof(App.Resources.Domain.Declaration))]
    public ICollection<TimelogBLLDto>? Timelogs { get; set; }
    [Display(Name = nameof(Assignments), Prompt = nameof(Assignments), ResourceType = typeof(App.Resources.Domain.Declaration))]
    public ICollection<AssignmentBLLDto>? Assignments { get; set; }
}