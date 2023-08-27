using Medical.System.Core.Enums;
using Medical.System.Core.Models.Entities;

namespace Medical.System.Core.Models.DTOs;

public class PatientDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; } 
    public DateTime BirthDate { get; set; }

    // Contact Information
    public Address Address { get; set; } = new Address();
    public string PhoneNumber { get; set; }
    public EmergencyContact EmergencyContact { get; set; } = new EmergencyContact();

    // Personal Information
    public string ProfilePhotoUrl { get; set; }
    public string Nationality { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public Gender Gender { get; set; }
}
