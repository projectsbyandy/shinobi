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

    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = LastName;

    [Required(ErrorMessage = "First name is required")]
    public string FirstName { get; set; } = FirstName;

    [Required]
    public int? Level { get; set; }
}
