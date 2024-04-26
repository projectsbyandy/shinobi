using Shinobi.Core.Models;
using Shinobi.Core.Models.Faker;

namespace Shinobi.FunctionApp.Repository.Internal;

public class FakeShinobiRepository : IShinobiRepository
{
    private IEnumerable<Ninja> _ninjas = Enumerable.Empty<Ninja>();
    
    public Ninja GetNinjaWith(int id)
    {
        return _ninjas.AsQueryable().First(ninja => ninja.Id == id);
    }
    
    public IEnumerable<Ninja> GetNinjas()
    {
        return _ninjas.Concat(new FakerNinja().Generate(12)).AsQueryable();
    }
}