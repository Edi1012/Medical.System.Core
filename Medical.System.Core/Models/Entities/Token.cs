using Medical.System.Core.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Medical.System.Core.Models.Entities;

[MongoCollectionName("revoked_tokens")]
public class RevokedToken
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();

    public string TokenID { get; set; }
    public Guid UserID { get; set; }

    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }

    public bool Revoked { get; set; } = false;
}
