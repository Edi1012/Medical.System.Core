using MongoDB.Bson;

namespace Medical.System.Core.Models.Entities;
public class Supplier
{
    public ObjectId Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}