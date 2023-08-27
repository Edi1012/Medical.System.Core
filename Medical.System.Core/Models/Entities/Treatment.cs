using Medical.System.Core.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Medical.System.Core.Models.Entities;

[MongoCollectionName("treatments")]
public class Treatment
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    /// <summary>
    /// Unique identifier for the treatment type.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the treatment.
    /// </summary>
    public string TreatmentName { get; set; }

    /// <summary>
    /// Description of the treatment.
    /// </summary>
    public string Description { get; set; }
    /// <summary>
    /// List of supply requirements needed for the treatment.
    /// </summary>
    public List<TreatmentSupplyRequirement> RequiredSupplies { get; set; }
}



