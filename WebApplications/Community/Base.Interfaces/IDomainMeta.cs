namespace Base.Interfaces;

public interface IDomainMeta
{
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }

    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    
    public string? SysNotes { get; set; }
}