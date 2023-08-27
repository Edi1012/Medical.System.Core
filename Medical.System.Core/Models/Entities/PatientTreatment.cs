using Medical.System.Core.Attributes;
using MongoDB.Bson;

namespace Medical.System.Core.Models.Entities;

[MongoCollectionName("patient_treatments")]
/// <summary>
/// Represents a treatment that a patient has received.
/// </summary>
public class PatientTreatments
{
    /// <summary>
    /// Unique identifier for the record.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Unique identifier for the patient.
    /// </summary>
    public Guid PatientId { get; set; }

    /// <summary>
    /// Unique identifier for the treatment type.
    /// </summary>
    public Guid TreatmentId { get; set; }

    /// <summary>
    /// Start date of the treatment.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// End date of the treatment.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// List of supplies used during the treatment.
    /// </summary>
    public List<TreatmentSupplyRequirement> UsedSupplies { get; set; }
}
