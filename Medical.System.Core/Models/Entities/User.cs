﻿using Medical.System.Core.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Medical.System.Core.Models.Entities;
public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; } // Paternal or Maternal Last Name
    public DateTime BirthDate { get; set; } // Date of Birth

    public string Email { get; set; }
    public Login Login { get; set; } = new Login();
    public List<Role> Roles { get; set; } = new List<Role>();

    // Additional Fields
    public Address Address { get; set; } = new Address();
    public string PhoneNumber { get; set; }
    public Gender Gender { get; set; }
    public EmergencyContact EmergencyContact { get; set; } = new EmergencyContact();
    public string ProfilePhotoUrl { get; set; }
    public string Nationality { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public string OfficialIdNumber { get; set; }

    // Other fields as needed
}


