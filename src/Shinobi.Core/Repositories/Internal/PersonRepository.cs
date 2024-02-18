using Microsoft.IdentityModel.Tokens;
using Shinobi.Core.Data;
using Shinobi.Core.Models;

namespace Shinobi.Core.Repositories.Internal;

public class PersonRepository : IPersonRepository
{
    private readonly ShinobiContext _shinobiContext;

    public PersonRepository(ShinobiContext shinobiContext)
    {
        _shinobiContext = shinobiContext;
    }

    public IEnumerable<Person> Get()
    {
        if (_shinobiContext.Persons.IsNullOrEmpty())
            return Enumerable.Empty<Person>();

        return _shinobiContext.Persons;
    }
    
    public Person? Get(int personId)
    {
        if (_shinobiContext.Persons.IsNullOrEmpty())
            return null;

        return _shinobiContext.Persons.ToList().Find(person => person.PersonId == personId);
    }
}