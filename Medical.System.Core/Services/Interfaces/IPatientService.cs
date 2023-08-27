using Medical.System.Core.Models.DTOs;
using Medical.System.Core.Models.Entities;

namespace Medical.System.Core.Services.Interfaces;

public interface IPatientService
{
    Task<IEnumerable<Patient>> GetAllPatientsAsync();
    Task<Patient> GetPatientByIdAsync(Guid id);
    Task<Patient> CreatePatientAsync(PatientDto patientDto);
    Task UpdatePatientAsync(Guid id,PatientDto patientDto);
    Task UpdatePatientByPathAsync(Guid id, Dictionary<string, object> updates);
    Task DeletePatientAsync(Guid id);
}
