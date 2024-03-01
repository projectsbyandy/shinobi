using Shinobi.Core.Data;
using Shinobi.Core.Models;
using Shinobi.Core.Repositories;
using Shinobi.Core.Repositories.Internal;

namespace Shinobi.Tests.MockHelpers;

public class INinjaRepositoryMock
{
    public static INinjaRepository GetMock(NinjaMockOptions ninjaMockOptions)
    {
        var ninjas = GenerateTestData(ninjaMockOptions);
        var dbContextMock = DbContextMock.GetMock<Ninja, ShinobiContext>(ninjas, x => x.Ninja);
        return new NinjaRepository(dbContextMock);
    }

    private static List<Ninja> GenerateTestData(NinjaMockOptions ninjaMockOptions)
    {
        if (ninjaMockOptions.ReturnEmpty)
            return new List<Ninja>();

        var count = ninjaMockOptions.ReturnSingle ? 1 : ninjaMockOptions.ReturnCount;
        
        List<Ninja> people = new();
        for (var index = 1; index <= count; index++)
        {                
            people.Add(new Ninja($"John{index}","Doe")
            {
                Id = new Random().Next(1, 100),
                Level = 3
            });
        }
        return people;
    }
}