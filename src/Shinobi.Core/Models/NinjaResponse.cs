namespace Shinobi.Core.Models;

public class NinjaResponse
{
    public IEnumerable<Ninja?> Ninjas { get; set; } = new List<Ninja>();
    public Skill? Skills { get; set; }
}