namespace Shinobi.Core.Models;

public class PersonResponse
{
    public IEnumerable<Person> Persons { get; set; } = new List<Person>();
    public Skills Skills { get; set; }
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
}