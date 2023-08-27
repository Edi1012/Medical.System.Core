namespace Medical.System.Core.Enums;

/// <summary>
/// Enumeration for the types of inventory inputs.
/// </summary>
public enum InputType
{
    PurchaseFromSupplier,
    InternalTransfer,
    ExternalTransfer,
    Donation,
    PatientReturn,
    Research
}