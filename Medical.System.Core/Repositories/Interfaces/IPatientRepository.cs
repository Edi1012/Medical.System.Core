using Medical.System.Core.Models.Entities;
using MongoDB.Driver;

namespace Medical.System.Core.Repositories.Interfaces;

public interface IPatientRepository : IGenericRepository<Patient>
{
    Task<IEnumerable<Patient>> GetAllAsync();
    Task<Patient> GetByIdAsync(Guid id);
    Task CreateAsync(Patient patient);
    Task UpdateAsync(Patient patient);
    Task UpdateFieldsAsync(Guid id, UpdateDefinition<Patient> updateDefinition);
    Task DeleteAsync(Guid id);
}
