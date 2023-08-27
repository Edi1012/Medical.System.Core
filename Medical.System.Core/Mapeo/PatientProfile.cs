using AutoMapper;
using Medical.System.Core.Models.DTOs;
using Medical.System.Core.Models.Entities;

namespace Medical.System.Core.Mapeo;

public class PatientProfile : Profile
{
    public PatientProfile()
    {
        CreateMap<Patient, PatientDto>().ReverseMap();
        CreateMap<PatientDto, Patient>().ReverseMap();
    }
}