namespace Medical.System.Core.Models.Entities;

/// <summary>
/// Represents a supply requirement for a treatment.
/// </summary>
public class TreatmentSupplyRequirement
{
    /// <summary>
    /// Unique identifier for the supply item.
    /// </summary>
    public Guid SupplyId { get; set; }

    /// <summary>
    /// Quantity of the supply item required.
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    /// Unit of measurement for the supply item (e.g., cm, ml, etc.).
    /// </summary>
    public string Unit { get; set; }
}