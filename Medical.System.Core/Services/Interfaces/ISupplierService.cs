using Medical.System.Core.Models.DTOs;
using Medical.System.Core.Models.Entities;

namespace Medical.System.Core.Services.Implementations
{
    public interface ISupplierService
    {
        Task<Supplier> Create(CreateSupplierDto supplier);
    }
}