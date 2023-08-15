﻿using Medical.System.Core.Models.Entities;
using Medical.System.Core.Repositories.Interfaces;
using Medical.System.Core.Services.Interfaces;
using MongoDB.Driver;

namespace Medical.System.Core.Repositories.Implementations;

public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
{
    private readonly IMongoCollection<Supplier> _collection;
    public SupplierRepository(IDatabaseResolverService databaseResolver) : base(databaseResolver, DatabaseTypes.MedicalSystem, "Suppliers")
    {
        _collection = databaseResolver[DatabaseTypes.MedicalSystem].GetColl<Supplier>("Suppliers");
    }
}
