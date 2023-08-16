using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Medical.System.Core.Models.Entities;

public class RevokedToken
{
    public ObjectId Id { get; set; }

    public string TokenID { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string UserID { get; set; }

    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }

    public bool Revoked { get; set; } = false;
}
