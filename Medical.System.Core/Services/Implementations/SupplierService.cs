using Medical.System.Core.Models.DTOs;
using Medical.System.Core.Models.Entities;
using Medical.System.Core.UnitOfWork;

namespace Medical.System.Core.Services.Implementations;

public class SupplierService : ISupplierService
{
    public IUnitOfWork UnitOfWork { get; }

    public SupplierService(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Supplier>> GetAll()
    {
        return await UnitOfWork.Supplier.GetAllAsync();
    }

    public async Task<Supplier> GetById(Guid id)
    {
        return await UnitOfWork.Supplier.GetByIdAsync(id);
    }

    public async Task<Supplier> Create(CreateSupplierDto supplierDTO)
    {

        Supplier supplier = new Supplier() {
            Name            = supplierDTO.Name,
            Address         = supplierDTO.Address,
            PhoneNumber     = supplierDTO.PhoneNumber,
            Email           = supplierDTO.Email,
        };       

        await UnitOfWork.Supplier.AddAsync(supplier);
        return supplier;
    }

    public async Task<Supplier> GetByIdAsync(Guid id)
    {
        var supplier = await UnitOfWork.Supplier.GetByIdAsync(id);
        return supplier;
    }
}
