using Bogus;

namespace Shinobi.Core.Models.Faker;

public class FakerNinja : Faker<Ninja>
{
    public FakerNinja()
    {
        var id = 1;

        UseSeed(1000)
            .CustomInstantiator(f => new Ninja(f.Person.FirstName, f.Person.LastName))
            .RuleFor(c => c.Id, _ => id++)
            .RuleFor(c => c.Level, f => --id);
    }
}