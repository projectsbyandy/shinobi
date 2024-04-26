using Shinobi.Core.Models;
using Shinobi.FunctionApp.Repository;

namespace Shinobi.FunctionApp.Services.Internal;

public class ShinobiService : IShinobiService
{
    private readonly IShinobiRepository _shinobiRepository;

    public ShinobiService(IShinobiRepository shinobiRepository)
    {
        _shinobiRepository = shinobiRepository;
    }

    public Ninja Get(int id)
    {
        return _shinobiRepository.GetNinjaWith(id);
    }
    
    public IEnumerable<Ninja> GetAll()
    {
        return _shinobiRepository.GetNinjas();
    }
}