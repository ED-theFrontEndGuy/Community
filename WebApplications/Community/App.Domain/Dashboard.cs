using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Dashboard : BaseEntity
{
    [MaxLength(128)] public string ConfigJson { get; set; } = default!;

    public Guid UserId { get; set; }
    public User? User { get; set; }
}