using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Medical.System.Core.Models.Entities;

public class RevokedToken
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string TokenID { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string UserID { get; set; }

    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }

    public bool Revoked { get; set; } = false;
}
