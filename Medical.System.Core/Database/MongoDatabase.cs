using NLog;
using MongoDB.Driver;

namespace Medical.System.Core.Database;

public class MongoDatabase : IMongo
{
    private readonly IMongoClient   MongoClient;
    private readonly IMongoDatabase Database;

    public IMongoClient GetClient() => MongoClient;

    public MongoDatabase(string DatabaseName, MongoClientSettings ClientSettings)
    {
        try
        {
            //TODO:2023-08-06:Remove hardcoded
            MongoClient         = new MongoClient("mongodb://localhost:27017/");

            Database            = MongoClient.GetDatabase(DatabaseName);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public IMongoCollection<T> GetColl<T>(string collName)
    {
        return Database.GetCollection<T>(collName);
    }
}
