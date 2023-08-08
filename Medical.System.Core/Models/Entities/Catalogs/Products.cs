using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Medical.System.Core.Models.Entities.Catalogs;

public class Products

{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public string CategoryID { get; set; }
}
