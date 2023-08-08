using Medical.System.Core.Models.Entities.Catalogs;
using Medical.System.Core.Services.Interfaces;
using MongoDB.Driver;

namespace Medical.System.Core.Services.Implementations;

public class CatalogsService : ICatalogsService
{
    private readonly IMongoCollection<User> _users;
    private const string DATABASE_NAME = "MedicalSystem";
    private const string COLLECTION_NAME = "Catalogs_user";

    public IDatabaseResolverService DatabaseResolverService { get; }

    public CatalogsService(IDatabaseResolverService DatabaseResolverService)
    {
        this.DatabaseResolverService = DatabaseResolverService;
        DatabaseResolverService.Init();
        DatabaseResolverService.GetDatabase(DatabaseTypes.Catalogs);
        _users = DatabaseResolverService.GetDatabase(DatabaseTypes.Catalogs).GetColl<User>(COLLECTION_NAME);
    }

    public async Task CreateUserAsync(User user)
    {
        await _users.InsertOneAsync(user);
    }

    public async Task<User> GetUserByIdAsync(string id)
    {
        return await _users.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        await _users.ReplaceOneAsync(x => x.Id == user.Id, user);
    }

    public async Task DeleteUserAsync(string id)
    {
        await _users.DeleteOneAsync(x => x.Id == id);
    }

    public async Task<bool> ExistUserNameAsync(string userName)
    {
        return await _users.CountDocumentsAsync(x => x.UserName == userName) != 0;
    }
}

