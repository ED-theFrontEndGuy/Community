using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.DAL.DTO;

public class TimelogDto : IDomainId
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(StartTime), ResourceType = typeof(App.Resources.Domain.TimeLog))]
    public DateTime StartTime { get; set; }
    [Display(Name = nameof(EndTime), ResourceType = typeof(App.Resources.Domain.TimeLog))]
    public DateTime? EndTime { get; set; }

    public Guid DeclarationId { get; set; }
    [Display(Name = nameof(Declaration), Prompt = nameof(Declaration), ResourceType = typeof(App.Resources.Domain.TimeLog))]
    public DeclarationDto? Declaration { get; set; }

    public Guid AssignmentId { get; set; }
    [Display(Name = nameof(Assignment), Prompt = nameof(Assignment), ResourceType = typeof(App.Resources.Domain.TimeLog))]
    public AssignmentDto? Assignment { get; set; }
}