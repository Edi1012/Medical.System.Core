using Medical.System.Core.Database;
using Medical.System.Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Medical.System.Core.Services.Implementations;

public class DatabaseResolverService : IDatabaseResolverService
{
    private readonly IVaultService VaultService;
    private readonly Dictionary<DatabaseTypes, IMongo> Databases = new();

    private bool Initialized = false;

    private MongoClientSettings MongoClientSettings;

    private readonly Dictionary<DatabaseTypes, string> DatebaseEnvVarRelation = new()
    {
        { DatabaseTypes.MedicalSystem,           "MONGO_DB_MEDICAL_SYSTEM_DATABASE_NAME" },
    };

    private readonly IConfiguration _databaseConfig;

    public DatabaseResolverService(IVaultService VaultService, IConfiguration databaseConfig)
    {
        this.VaultService = VaultService;
        this._databaseConfig = databaseConfig;
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
            Databases.Add(rt, CreateDatabase(rt));

        return Databases[rt];
    }
    private IMongo CreateDatabase(DatabaseTypes databaseType)
    {
        string configKey = DatebaseEnvVarRelation[databaseType];
        var DatabaseName = _databaseConfig[configKey];

        if (string.IsNullOrEmpty(DatabaseName))
            throw new ApplicationException($"Configuration key {configKey} is not set");

        return new MongoDatabase(DatabaseName, MongoClientSettings);
    }

    public IMongo this[DatabaseTypes rt] { get => GetDatabase(rt); }
}