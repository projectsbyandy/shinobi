using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Swashbuckle.AspNetCore.Annotations;

namespace Shinobi.Core.Models;

public class Ninja
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [SwaggerSchema(ReadOnly = true)]
    public int Id { get; set; }

    [Required]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    public int? Level { get; set; }
}
