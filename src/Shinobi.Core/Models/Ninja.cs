using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Swashbuckle.AspNetCore.Annotations;

namespace Shinobi.Core.Models;

public record Ninja(string FirstName, string LastName)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [SwaggerSchema(ReadOnly = true)]
    public int Id { get; set; }
    public string LastName { get; set; } = LastName;
    public string FirstName { get; set; } = FirstName;

    [Required]
    public int? Level { get; set; }
}
