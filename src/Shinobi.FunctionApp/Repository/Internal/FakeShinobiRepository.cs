using Shinobi.Core.Models;
using Shinobi.Core.Models.Faker;

namespace Shinobi.FunctionApp.Repository.Internal;

public class FakeShinobiRepository : IShinobiRepository
{
    private IEnumerable<Ninja> _ninjas = new List<Ninja>()
    {
        new("Pete","Wrong")
        {
            Id = 999,
            Level = 2
        }
    };

    public Ninja? GetNinjaWith(int id)
    {
        return _ninjas.FirstOrDefault(ninja => ninja.Id == id);
    }
    
    public IEnumerable<Ninja> GetNinjas()
    {
        return _ninjas;
    }

    public IEnumerable<Ninja> Seed(int numberOfNinjas)
    {
        var currentId = _ninjas.Last().Id + 1;
        var generatedData = new FakerNinja(currentId).Generate(numberOfNinjas);
        _ninjas = _ninjas.Concat(generatedData);

        return generatedData;
    }
}