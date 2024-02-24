using System.ComponentModel.DataAnnotations;

namespace Shinobi.Core.Models;

public class Person
{
    [Key]
    public int PersonId { get; set; }

    public string? LastName { get; set; }

    public string? FirstName { get; set; }
}
