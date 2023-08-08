using Medical.System.Core.Services.Interfaces;
using MongoDB.Driver;

namespace Medical.System.Core.Services.Implementations;

public class VaultService : IVaultService
{
    public MongoClientSettings GetMongoDBSettings(string connectionString = "mongodb://localhost:27017/")
    {
        try
        {
            var MongoClientSettings = new MongoClientSettings() { Server = new MongoServerAddress(connectionString) };
            return MongoClientSettings;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
