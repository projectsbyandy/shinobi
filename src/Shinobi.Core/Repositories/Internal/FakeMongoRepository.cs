using EphemeralMongo;
using MongoDB.Driver;
using Shinobi.Core.Models;

namespace Shinobi.Core.Repositories.Internal;

public class FakeMongoRepository : INinjaRepository
{
    private IMongoDatabase _database;

    public FakeMongoRepository()
    {
        Setup();
    }
    
    private void Setup()
    {
        var options = new MongoRunnerOptions
        {
            UseSingleNodeReplicaSet = true, // Default: false
            StandardOuputLogger = line => Console.WriteLine(line), // Default: null
            StandardErrorLogger = line => Console.WriteLine(line), // Default: null
            DataDirectory = "/", // Default: null
            BinaryDirectory = $"/runtimes/osx-x64/native/mongodb/bin/", // Default: null
            ConnectionTimeout = TimeSpan.FromSeconds(10), // Default: 30 seconds
            ReplicaSetSetupTimeout = TimeSpan.FromSeconds(5), // Default: 10 seconds
            AdditionalArguments = "--quiet", // Default: null
            MongoPort = 27017, // Default: random available port
        };

        using var runner = MongoRunner.Run(options);
        _database = new MongoClient(runner.ConnectionString).GetDatabase("default");

        // Do something with the database
        _database.CreateCollection("ninja");

        // Import a collection. Full method signature:
        // Import(string database, string collection, string inputFilePath, string? additionalArguments = null, bool drop = false)
        runner.Import("default", "Ninja", "Ninja.json");
    }

    public IList<Ninja> Get()
    {
        var ninjas = _database.GetCollection<Ninja>("ninja");
        return null;
    }

    public Ninja? Get(int id)
    {
        throw new NotImplementedException();
    }

    public void Add(Ninja ninja)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}