using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class ContactType : BaseEntity
{
    [MaxLength(128)]
    public string ContactTypeName { get; set; } = default!;

    public ICollection<Contact>? Contacts { get; set; }
}