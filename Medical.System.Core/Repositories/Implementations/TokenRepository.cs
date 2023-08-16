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

    public async Task AddAsync(RevokedToken entity)
    {
        var filter = Builders<RevokedToken>.Filter.Eq(r => r.UserID, entity.UserID);
        var existingToken = await _collection.Find(filter).FirstOrDefaultAsync();

        if (existingToken == null)
        {
            // UserID not found, so insert.
            await _collection.InsertOneAsync(entity);
        }
        else if (existingToken.Revoked)
        {
            // UserID found and its Revoked is true, so update.
            var update = Builders<RevokedToken>.Update
                .Set(r => r.TokenID, entity.TokenID)
                .Set(r => r.ValidFrom, entity.ValidFrom)
                .Set(r => r.ValidTo, entity.ValidTo)
                .Set(r => r.Revoked, entity.Revoked);

            await _collection.UpdateOneAsync(filter, update);
        }
        // If UserID found and its Revoked is false, do nothing.
    }

    public async Task<RevokedToken> GetByUserIdAsync(Guid UserId) =>
    await _collection.Find(Builders<RevokedToken>.Filter.Eq("UserID", UserId)).FirstOrDefaultAsync();
}
