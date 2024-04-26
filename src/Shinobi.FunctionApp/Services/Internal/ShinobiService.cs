using System.Net;
using Shinobi.Core.Models;
using Shinobi.FunctionApp.Models;
using Shinobi.FunctionApp.Repository;

namespace Shinobi.FunctionApp.Services.Internal;

public class ShinobiService : IShinobiService
{
    private readonly IShinobiRepository _shinobiRepository;

    public ShinobiService(IShinobiRepository shinobiRepository)
    {
        _shinobiRepository = shinobiRepository;
    }

    public ShinobiServiceResponse<Ninja> Get(int id)
    {
        var ninja = _shinobiRepository.GetNinjaWith(id);

        if (ninja is null)
            return new ShinobiServiceResponse<Ninja>(HttpStatusCode.NotFound)
            {
                Message = "Ninja was not found"
            };
        
        return new ShinobiServiceResponse<Ninja>(HttpStatusCode.OK)
        {
            Data = ninja
        };
    }
    
    public ShinobiServiceResponse<IEnumerable<Ninja>> GetAll()
    {
        return new ShinobiServiceResponse<IEnumerable<Ninja>>(HttpStatusCode.OK)
        {
            Data = _shinobiRepository.GetNinjas()
        };
    }

    public ShinobiServiceResponse<IEnumerable<Ninja>> Seed(int numberOfNinjas)
    {
        return new ShinobiServiceResponse<IEnumerable<Ninja>>(HttpStatusCode.Created)
        {
            Data = _shinobiRepository.Seed(numberOfNinjas)
        };
    }
}