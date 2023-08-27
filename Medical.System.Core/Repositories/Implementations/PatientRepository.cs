using Medical.System.Core.Helpers;
using Medical.System.Core.Models.Entities;
using Medical.System.Core.Repositories.Interfaces;
using Medical.System.Core.Services.Interfaces;
using MongoDB.Driver;

namespace Medical.System.Core.Repositories.Implementations;

public class PatientRepository : GenericRepository<Patient>,IPatientRepository
{
    private readonly IMongoCollection<Patient> _patients;

    public PatientRepository(IDatabaseResolverService databaseResolver) : base(databaseResolver, DatabaseTypes.MedicalSystem, MongoCollectionHelper.GetCollectionName<User>())
    {
        _patients = databaseResolver[DatabaseTypes.MedicalSystem].GetColl<Patient>(MongoCollectionHelper.GetCollectionName<Patient>());
    }

    public async Task<IEnumerable<Patient>> GetAllAsync()
    {
        return await _patients.Find(patient => true).ToListAsync();
    }

    public async Task<Patient> GetByIdAsync(Guid id)
    {
        return await _patients.Find(patient => patient.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Patient patient)
    {
        await _patients.InsertOneAsync(patient);
    }

    public async Task UpdateAsync(Patient patient)
    {
        await _patients.ReplaceOneAsync(p => p.Id == patient.Id, patient);
    }

    public async Task UpdateFieldsAsync(Guid id, UpdateDefinition<Patient> updateDefinition)
    {
        var filter = Builders<Patient>.Filter.Eq(p => p.Id, id);
        await _patients.UpdateOneAsync(filter, updateDefinition);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _patients.DeleteOneAsync(patient => patient.Id == id);
    }
}

