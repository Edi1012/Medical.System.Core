using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Vifaru.Core.Database;
using Medical.System.Core.Services.Interfaces;

namespace Medical.System.Core.Services;

public class DatabaseResolverService : IDatabaseResolverService
{
    private readonly IVaultService VaultService;
    private readonly Dictionary<DatabaseTypes, IMongo> Databases = new();

    private bool Initialized = false;

    private MongoClientSettings MongoClientSettings;

    private readonly Dictionary<DatabaseTypes, string> DatebaseEnvVarRelation = new()
    {
        { DatabaseTypes.Catalogs,           "MONGO_DB_CATALOGS_DATABASE_NAME" },
    };

    public DatabaseResolverService(IVaultService VaultService)
    {
        this.VaultService = VaultService;
    }

    public async Task<IDatabaseResolverService> Init()
    {
        if (Initialized)
            return this;

        Initialized = true;
        MongoClientSettings = VaultService.GetMongoDBSettings();

        return this;
    }

    public IMongo GetDatabase(DatabaseTypes rt)
    {
        if (!Initialized)
            throw new ApplicationException("Resolver not initialized");

        if (!Databases.ContainsKey(rt))
            Databases.Add(rt, CreateDatabase(DatebaseEnvVarRelation[rt]));

        return Databases[rt];
    }

    private IMongo CreateDatabase(string EnvVarDatabaseName)
    {
        //TODO:2023-08-06:add envar 
        var DatabaseName = Environment.GetEnvironmentVariable(EnvVarDatabaseName);

        DatabaseName = "MedicalSystem";

        if (string.IsNullOrEmpty(DatabaseName))
            throw new ApplicationException($"Environment variable {EnvVarDatabaseName} is not set");

        return new MongoDatabase(DatabaseName, MongoClientSettings);
    }

    public IMongo this[DatabaseTypes rt] { get => GetDatabase(rt); }
}