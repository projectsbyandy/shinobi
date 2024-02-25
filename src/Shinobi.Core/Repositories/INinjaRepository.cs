using Shinobi.Core.Models;

namespace Shinobi.Core.Repositories;

public interface INinjaRepository
{
    IList<Ninja> Get();
    Ninja? Get(int id);
    void Add(Ninja ninja);
    void Delete(int id);
}