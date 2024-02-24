using System.ComponentModel.DataAnnotations;

namespace Shinobi.Core.Models;

public class Skill
{
    [Key]
    public int? Level { get; set; }

    public string? Details { get; set; }
}
