using Medical.System.Core.Attributes;
using Medical.System.Core.Enums;

namespace Medical.System.Core.Models.Entities;

[MongoCollectionName("treatments")]
/// <summary>
/// Represents an inventory input record.
/// </summary>
public class InventoryInput
{
    /// <summary>
    /// Date of the inventory input.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Quantity of items added.
    /// </summary>
    public int InputQuantity { get; set; }

    /// <summary>
    /// Type of input.
    /// </summary>
    public InputType InputType { get; set; }

    /// <summary>
    /// Additional notes about the input.
    /// </summary>
    public string Notes { get; set; }
}
