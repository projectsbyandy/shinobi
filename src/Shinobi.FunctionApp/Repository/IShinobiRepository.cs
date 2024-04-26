using Shinobi.Core.Models;

namespace Shinobi.FunctionApp.Repository;

public interface IShinobiRepository
{
    public Ninja GetNinjaWith(int id);
    public IEnumerable<Ninja> GetNinjas();
}