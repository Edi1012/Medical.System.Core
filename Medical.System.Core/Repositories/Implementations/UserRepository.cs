using Medical.System.Core.Models.Entities.Catalogs;
using Medical.System.Core.Repositories.Implementations;
using Medical.System.Core.Repositories.Interfaces;
using Medical.System.Core.Services.Interfaces;
using MongoDB.Driver;

namespace Medical.System.Core.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{

    private readonly IMongoCollection<User> _collection;


    public UserRepository(IDatabaseResolverService databaseResolver) : base(databaseResolver, DatabaseTypes.MedicalSystem, "Catalogs_user")
    {
        _collection = databaseResolver[DatabaseTypes.MedicalSystem].GetColl<User>("Catalogs_user");
    }

    public async Task<bool> ExistUserNameAsync(string userName) =>
    await _collection.CountDocumentsAsync(x => x.Login.Username == userName) != 0;
}
