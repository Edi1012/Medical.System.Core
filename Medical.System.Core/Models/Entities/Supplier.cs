using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Medical.System.Core.Models.Entities;
public class Supplier
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}