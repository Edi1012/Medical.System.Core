using Medical.System.Core.Database;

namespace Medical.System.Core.Services.Interfaces;


public enum DatabaseTypes
{
    Catalogs
}

public interface IDatabaseResolverService
{
    IMongo this[DatabaseTypes rt] { get; }

    IMongo GetDatabase(DatabaseTypes rt);
    Task<IDatabaseResolverService> Init();
}