using Medical.System.Core.Attributes;
using MongoDB.Bson;

namespace Medical.System.Core.Models.Entities;

[MongoCollectionName("supplies")]
/// <summary>
/// Represents a supply item in the catalog.
/// </summary>
public class Supply
{
    /// <summary>
    /// Unique identifier for the supply item.
    /// </summary>
    public ObjectId Id { get; set; }

    /// <summary>
    /// Name of the supply.
    /// </summary>
    public string SupplyName { get; set; }

    /// <summary>
    /// Description of the supply.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Price of the supply item.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Available quantity in the inventory.
    /// </summary>
    public int AvailableQuantity { get; set; }

    /// <summary>
    /// List of inventory input records.
    /// </summary>
    public List<InventoryInput> InventoryInputs { get; set; }

    /// <summary>
    /// List of inventory output records.
    /// </summary>
    public List<InventoryOutput> InventoryOutputs { get; set; }
}