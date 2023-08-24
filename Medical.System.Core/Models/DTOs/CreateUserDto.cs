using Medical.System.Core.Enums;

namespace Medical.System.Core.Models.DTOs;

public class CreateUserDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public LoginDTO Login { get; set; }
    public List<string> Roles { get; set; }
    public AddressDTO Address { get; set; }
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }
    public EmergencyContactDTO EmergencyContact { get; set; }
    public string ProfilePhotoUrl { get; set; }
    public string Nationality { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public string OfficialIdNumber { get; set; }
}



