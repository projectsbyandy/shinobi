namespace Shinobi.Core.Models;

public class PersonResponse
{
    public IEnumerable<Person?> People { get; set; } = new List<Person>();
    public Skills? Skills { get; set; }
}