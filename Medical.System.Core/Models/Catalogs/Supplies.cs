using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Vifaru.Core.Models.FixedIncome;

[BsonIgnoreExtraElements]
public class Supplies
{
    [BsonId]
    public Guid Id { get; set; }
    public string Name { get; set; }
}

