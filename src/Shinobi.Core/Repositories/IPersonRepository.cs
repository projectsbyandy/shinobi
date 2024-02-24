using Shinobi.Core.Models;

namespace Shinobi.Core.Repositories;

public interface IPersonRepository
{
    IList<Person>? Get();
    Person? Get(int personId);
    bool Post(Person person);
}