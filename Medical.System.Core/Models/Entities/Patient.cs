using Medical.System.Core.Attributes;
using Medical.System.Core.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Medical.System.Core.Models.Entities;

[MongoCollectionName("patients")]
public class Patient
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; } // Paternal or Maternal Last Name
    public DateTime BirthDate { get; set; } // Date of Birth

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
