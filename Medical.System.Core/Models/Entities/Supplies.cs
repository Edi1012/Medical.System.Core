using MongoDB.Bson.Serialization.Attributes;

namespace Medical.System.Core.Models.Entities;

[BsonIgnoreExtraElements]
public class Supplies
{
    [BsonId]
    public Guid Id { get; set; }
    public string Name { get; set; }
}

