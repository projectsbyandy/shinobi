using Shinobi.Core.Models;
using Shinobi.FunctionApp.Models;

namespace Shinobi.FunctionApp.Services;

public interface IShinobiService
{
    /// <summary>
    /// Returns Ninja details
    /// </summary>
    /// <param name="id">Ninja id to retrieve</param>
    public ShinobiServiceResponse<Ninja> Get(int id);
    
    /// <summary>
    /// <returns>
    /// All Ninja details
    /// </returns>
    /// </summary>
    public ShinobiServiceResponse<IEnumerable<Ninja>> GetAll();
    
    /// <summary>
    /// <returns>
    /// Generated random Ninja
    /// </returns>
    /// <param name="numberOfNinjas">Number of Ninja to seed</param>
    /// </summary>
    public ShinobiServiceResponse<IEnumerable<Ninja>> Seed(int numberOfNinjas);

}