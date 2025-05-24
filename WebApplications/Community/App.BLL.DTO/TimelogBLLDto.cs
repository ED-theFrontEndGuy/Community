using System.ComponentModel.DataAnnotations;
using Base.Interfaces;

namespace App.BLL.DTO;

public class TimelogBLLDto : IDomainId
{
    public Guid Id { get; set; }
    
    [Display(Name = nameof(StartTime), ResourceType = typeof(App.Resources.Domain.TimeLog))]
    public DateTime StartTime { get; set; }
    [Display(Name = nameof(EndTime), ResourceType = typeof(App.Resources.Domain.TimeLog))]
    public DateTime? EndTime { get; set; }
    
    public TimeSpan? Duration { get; set; }

    public Guid DeclarationId { get; set; }
    [Display(Name = nameof(Declaration), Prompt = nameof(Declaration), ResourceType = typeof(App.Resources.Domain.TimeLog))]
    public DeclarationBLLDto? Declaration { get; set; }

    public Guid AssignmentId { get; set; }
    [Display(Name = nameof(Assignment), Prompt = nameof(Assignment), ResourceType = typeof(App.Resources.Domain.TimeLog))]
    public AssignmentBLLDto? Assignment { get; set; }
}