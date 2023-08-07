using MongoDB.Driver;

namespace Medical.System.Core.Database;

public interface IMongo
{
    IMongoClient            GetClient();
    IMongoCollection<T>     GetColl<T>(string collName);
}
