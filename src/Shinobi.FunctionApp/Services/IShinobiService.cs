using Shinobi.Core.Models;

namespace Shinobi.FunctionApp.Services;

public interface IShinobiService
{
    public Ninja Get(int id);
    public IEnumerable<Ninja> GetAll();
}