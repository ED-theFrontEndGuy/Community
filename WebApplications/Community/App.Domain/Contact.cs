using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Contact : BaseEntity
{
    [MaxLength(128)]
    public string Value { get; set; } = default!;
    
    public Guid ContactTypeId { get; set; }
    public ContactType? ContactType { get; set; }
    
    public Guid PersonId { get; set; }
    public Person? Person { get; set; }
}