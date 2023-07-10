using MongoDB.Driver;

namespace Vifaru.Core.Database;

public interface IMongo
{
    IMongoClient            GetClient();
    IMongoCollection<T>     GetColl<T>(string collName);
    Task<List<string>>      GetIndexes<T>(string CollectionName);
}
