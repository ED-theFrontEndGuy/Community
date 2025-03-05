using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Person : BaseEntity
{
    [MaxLength(128)]
    public string PersonName { get; set; } = default!;

    public ICollection<Contact>? Contacts { get; set; }
}