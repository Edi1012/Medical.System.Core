using Medical.System.Core.Models.Entities;
using Medical.System.Core.Repositories.Interfaces;
using Medical.System.Core.Services.Interfaces;
using MongoDB.Driver;

namespace Medical.System.Core.Repositories.Implementations;

public class TokenRepository : GenericRepository<RevokedToken>, ITokenRepository
{

    private readonly IMongoCollection<RevokedToken> _collection;


    public TokenRepository(IDatabaseResolverService databaseResolver) : base(databaseResolver, DatabaseTypes.MedicalSystem, "RevokedTokens")
    {
        _collection = databaseResolver[DatabaseTypes.MedicalSystem].GetColl<RevokedToken>("RevokedTokens");
    }
}
