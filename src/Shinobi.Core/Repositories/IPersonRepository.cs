using Shinobi.Core.Models;

namespace Shinobi.Core.Repositories;

public interface IPersonRepository
{
    IEnumerable<Person>? Get();
    Person? Get(int personId);
}