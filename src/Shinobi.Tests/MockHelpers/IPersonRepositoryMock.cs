using Shinobi.Core.Data;
using Shinobi.Core.Models;
using Shinobi.Core.Repositories;
using Shinobi.Core.Repositories.Internal;

namespace Shinobi.Tests.MockHelpers;

public class IPersonRepositoryMock
{
    public static IPersonRepository GetMock(PersonMockOptions personMockOptions)
    {
        var people = GenerateTestData(personMockOptions);
        var dbContextMock = DbContextMock.GetMock<Person, ShinobiContext>(people, x => x.Persons);
        return new PersonRepository(dbContextMock);
    }

    private static List<Person> GenerateTestData(PersonMockOptions personMockOptions)
    {
        if (personMockOptions.ReturnEmpty)
            return new List<Person>();
        
        List<Person> people = new();
        for (var index = 1; index <= personMockOptions.ReturnCount; index++)
        {                
            people.Add(new Person()
            {
                PersonId= new Random().Next(1, 100),
                FirstName = $"John{index}",
                LastName = "Doe"
            });
        }
        return people;
    }
}