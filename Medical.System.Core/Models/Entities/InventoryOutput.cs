using Medical.System.Core.Attributes;
using Medical.System.Core.Enums;

namespace Medical.System.Core.Models.Entities;

[MongoCollectionName("inventory_outputs")]
/// <summary>
/// Represents an inventory output record.
/// </summary>
public class InventoryOutput
{
    /// <summary>
    /// Date of the inventory output.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Quantity of items removed.
    /// </summary>
    public int OutputQuantity { get; set; }

    /// <summary>
    /// Type of output.
    /// </summary>
    public OutputType OutputType { get; set; }

    /// <summary>
    /// Additional notes about the output.
    /// </summary>
    public string Notes { get; set; }
}
