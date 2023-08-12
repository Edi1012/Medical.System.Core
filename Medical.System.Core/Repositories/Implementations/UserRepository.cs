using Medical.System.Core.Models.DTOs;
using Medical.System.Core.Models.Entities;
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

    public async Task<bool> Loggin(LoginDTO LogginDTO) =>
    await _collection.CountDocumentsAsync(x => x.Login.Username == LogginDTO.Username && x.Login.PasswordHash == LogginDTO.PasswordHash) != 0;
}
