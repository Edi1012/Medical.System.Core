using NLog;
using MongoDB.Driver;

namespace Vifaru.Core.Database;

public class MongoDatabase : IMongo
{
    private readonly IMongoClient   MongoClient;
    private readonly IMongoDatabase Database;
    private readonly ILogger        Logger;

    public IMongoClient GetClient() => MongoClient;

    public MongoDatabase(string DatabaseName, MongoClientSettings ClientSettings)
    {
        try
        {
            Logger              = LogManager.GetCurrentClassLogger();
            MongoClient         = new MongoClient(ClientSettings);

            Database            = MongoClient.GetDatabase(DatabaseName);

        }
        catch (Exception ex)
        {
            Logger.Error(ex, "Error Starting MongoDatabase");
        }
    }

    public IMongoCollection<T> GetColl<T>(string collName)
    {
        return Database.GetCollection<T>(collName);
    }

    public async Task<List<string>> GetIndexes<T>(string CollectionName)
    {
        var Collection = GetColl<T>(CollectionName);

        return (await (await Collection.Indexes.ListAsync())
            .ToListAsync()).Select(d => d.GetElement("name").Value.ToString())
            .ToList();
    }
}
