using Shinobi.Core.Models;
using Shinobi.FunctionApp.Models;

namespace Shinobi.FunctionApp.Services;

public interface IShinobiService
{
    public ShinobiServiceResponse<Ninja> Get(int id);
    public ShinobiServiceResponse<IEnumerable<Ninja>> GetAll();
    public ShinobiServiceResponse<IEnumerable<Ninja>> Seed(int numberOfNinjas);

}