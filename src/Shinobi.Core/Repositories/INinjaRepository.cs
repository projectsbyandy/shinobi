using Shinobi.Core.Models;

namespace Shinobi.Core.Repositories;

public interface INinjaRepository
{
    IList<Ninja>? Get();
    Ninja? Get(int id);
    bool Register(Ninja ninja);
}