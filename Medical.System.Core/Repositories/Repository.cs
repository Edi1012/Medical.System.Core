﻿using Medical.System.Core.Repositories.Interfaces;
using Medical.System.Core.Services.Interfaces;
using MongoDB.Driver;

namespace Medical.System.Core.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection;

    public Repository(IDatabaseResolverService databaseResolver, DatabaseTypes databaseType, string collectionName)
    {
        _collection = databaseResolver[databaseType].GetColl<T>(collectionName);
    }

    public async Task<IEnumerable<T>> GetAllAsync() =>
        await _collection.Find(x => true).ToListAsync();

    public async Task<T> GetByIdAsync(string id) =>
        await _collection.Find(Builders<T>.Filter.Eq("Id", id)).FirstOrDefaultAsync();

    public async Task AddAsync(T entity) =>
        await _collection.InsertOneAsync(entity);

    public async Task UpdateAsync(T entity) =>
        await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("Id", (entity as dynamic).Id), entity);

    public async Task DeleteAsync(string id) =>
        await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("Id", id));
}