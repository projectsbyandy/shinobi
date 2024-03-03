using Microsoft.EntityFrameworkCore;
using Shinobi.Core.Models;

namespace Shinobi.Core.Data;

public class MockDataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var shinobiDbContext = new ShinobiDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ShinobiDbContext>>());
        
        if (shinobiDbContext.Ninja.Any())
            return;
            
        shinobiDbContext.Ninja.AddRange(
            new Ninja("Guybrush", "Threepwood")
            {
                Id = 1,
                Level = 1
            },
            new Ninja("Sam", "Max")
            {
                Id = 2,
                Level = 2
            }
        );
        
        shinobiDbContext.Skill.AddRange(
            new Skill()
            {
                Level = 1,
                Details = "Flying Dragon"
            },
            new Skill()
            {
                Level = 2,
                Details = "Fiery Fish"
            });

        shinobiDbContext.SaveChanges();
    }
}