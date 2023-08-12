using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Medical.System.Core.Models.Entities;

public class Role
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Name { get; set; }
}
