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

    public IList<Person>? Get()
    {
        if (_shinobiContext.Persons.IsNullOrEmpty())
            return null;

        return _shinobiContext.Persons.ToList();
    }
    
    public Person? Get(int personId)
    { 
        var person = _shinobiContext.Persons.ToList().FirstOrDefault(person => person.PersonId == personId) ?? null;

        return person;
    }

    public bool Post(Person person)
    {
        if (Get(person.PersonId) is not null)
            return false;
        
        var test = _shinobiContext.Persons.Add(person);
        _shinobiContext.SaveChanges();

        return true;
    }
}