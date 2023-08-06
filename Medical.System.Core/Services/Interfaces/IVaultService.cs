using MongoDB.Driver;

namespace Medical.System.Core.Services.Interfaces;


public interface IVaultService
{
    MongoClientSettings GetMongoDBSettings(string connectionString = "mongodb://localhost:27017/");
}

