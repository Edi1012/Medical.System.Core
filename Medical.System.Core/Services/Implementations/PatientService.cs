using AutoMapper;
using Medical.System.Core.Exceptions;
using Medical.System.Core.Models.DTOs;
using Medical.System.Core.Models.Entities;
using Medical.System.Core.Repositories.Interfaces;
using Medical.System.Core.Services.Interfaces;
using MongoDB.Driver;

namespace Medical.System.Core.Services.Implementations;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;

    public PatientService(IPatientRepository patientRepository, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
    {
        return await _patientRepository.GetAllAsync();
    }

    public async Task<Patient> GetPatientByIdAsync(Guid id)
    {
        return await _patientRepository.GetByIdAsync(id);
    }

    public async Task<Patient> CreatePatientAsync(PatientDto patientDto)
    {

        var patient = _mapper.Map<Patient>(patientDto);
        await _patientRepository.CreateAsync(patient);
        return patient;
    }

    public async Task UpdatePatientAsync(Guid id,PatientDto patientDto)
    {
        var patient = _mapper.Map<Patient>(patientDto);
        patient.Id = id;
        var existingPatient = await GetPatientByIdAsync(id);
        if (existingPatient == null)
        {
            throw new NotFoundException("Not exist");
        }

        await _patientRepository.UpdateAsync(patient);
    }

    // Método para actualizar campos anidados
    public async Task UpdatePatientByPathAsync(Guid id, Dictionary<string, object> updates)
    {
        var existingPatient = await GetPatientByIdAsync(id);
        if (existingPatient == null)
        {
            throw new NotFoundException("Patient not found");
        }

        var updateDefinitions = new List<UpdateDefinition<Patient>>();
        foreach (var update in updates)
        {
            updateDefinitions.Add(Builders<Patient>.Update.Set(update.Key, update.Value));
        }

        var combinedUpdate = Builders<Patient>.Update.Combine(updateDefinitions);

        if (updateDefinitions.Count > 0)
        {
            await _patientRepository.UpdateFieldsAsync(id, combinedUpdate);
        }
    }


    public async Task DeletePatientAsync(Guid id)
    {
        var existingPatient = await GetPatientByIdAsync(id);
        if (existingPatient == null)
        {
            throw new NotFoundException("Not exist");
        }

        await _patientRepository.DeleteAsync(id);
    }
}

