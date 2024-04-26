using Bogus;

namespace Shinobi.Core.Models.Faker;

public class FakerNinja : Faker<Ninja>
{
    public FakerNinja()
    {
        var id = 1;

        UseSeed(id)
            .CustomInstantiator(f => new Ninja(f.Person.FirstName, f.Person.LastName))
            .RuleFor(c => c.Id, f => f.IndexFaker++)
            .RuleFor(c => c.Level, f => --f.IndexFaker);
    }
}