using Microsoft.IdentityModel.Tokens;
using Shinobi.Core.Data;
using Shinobi.Core.Models;

namespace Shinobi.Core.Repositories.Internal;

public class NinjaRepository : INinjaRepository
{
    private readonly ShinobiDbContext _shinobiDbContext;

    public NinjaRepository(ShinobiDbContext shinobiDbContext)
    {
        _shinobiDbContext = shinobiDbContext;
    }

    public IList<Ninja> Get()
    {
        return _shinobiDbContext.Ninja.IsNullOrEmpty() 
            ? Enumerable.Empty<Ninja>().ToList() 
            : _shinobiDbContext.Ninja.ToList();
    }
    
    public Ninja? Get(int id)
    { 
        return _shinobiDbContext.Ninja.ToList().FirstOrDefault(ninja => ninja.Id == id) ?? null;;
    }

    public void Add(Ninja ninja)
    { 
        _shinobiDbContext.Ninja.Add(ninja);
        _shinobiDbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        _shinobiDbContext.Ninja.Remove(_shinobiDbContext.Ninja.Single(ninja => ninja.Id == id));
        _shinobiDbContext.SaveChanges();
    }
}