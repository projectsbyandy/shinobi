using Microsoft.IdentityModel.Tokens;
using Shinobi.Core.Data;
using Shinobi.Core.Models;

namespace Shinobi.Core.Repositories.Internal;

public class NinjaRepository : INinjaRepository
{
    private readonly ShinobiContext _shinobiContext;

    public NinjaRepository(ShinobiContext shinobiContext)
    {
        _shinobiContext = shinobiContext;
    }

    public IList<Ninja> Get()
    {
        return _shinobiContext.Ninja.IsNullOrEmpty() 
            ? Enumerable.Empty<Ninja>().ToList() 
            : _shinobiContext.Ninja.ToList();
    }
    
    public Ninja? Get(int id)
    { 
        return _shinobiContext.Ninja.ToList().FirstOrDefault(ninja => ninja.Id == id) ?? null;;
    }

    public void Register(Ninja ninja)
    {
         _shinobiContext.Ninja.Add(ninja);
        _shinobiContext.SaveChanges();
    }
}