﻿using Medical.System.Core.Models.Catalogs;
using Medical.System.Core.Repositories.Interfaces;
using Medical.System.Core.Services.Interfaces;
using MongoDB.Driver;

namespace Medical.System.Core.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _collection;

    public UserRepository(IDatabaseResolverService databaseResolver)
    {
        _collection = databaseResolver[DatabaseTypes.Catalogs].GetColl<User>("Catalogs_user");
    }

    public async Task<IEnumerable<User>> GetAllAsync() =>
        await _collection.Find(x => true).ToListAsync();

    public async Task<User> GetByIdAsync(string id) =>
        await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task AddAsync(User user) =>
        await _collection.InsertOneAsync(user);

    public async Task UpdateAsync(User user) =>
        await _collection.ReplaceOneAsync(x => x.Id == user.Id, user);

    public async Task DeleteAsync(string id) =>
        await _collection.DeleteOneAsync(x => x.Id == id);
}