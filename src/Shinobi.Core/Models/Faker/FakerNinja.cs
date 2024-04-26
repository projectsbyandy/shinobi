using Bogus;

namespace Shinobi.Core.Models.Faker;

public sealed class FakerNinja : Faker<Ninja>
{
    public FakerNinja(int i)
    {
        UseSeed(2312313)
            .CustomInstantiator(f => new Ninja(f.Person.FirstName, f.Person.LastName))
            .RuleFor(c => c.Id, f => f.IndexGlobal )
            .RuleFor(c => c.Level, f => f.IndexGlobal );
    }
}