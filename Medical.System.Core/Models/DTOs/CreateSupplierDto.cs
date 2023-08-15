using Medical.System.Core.Models.Entities;
using MongoDB.Bson.Serialization.Attributes;

namespace Medical.System.Core.Models.DTOs;

public class CreateSupplierDto
{
    public string Name { get; set; }
    public Address Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}
