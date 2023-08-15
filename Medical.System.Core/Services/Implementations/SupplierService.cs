using Medical.System.Core.Models.DTOs;
using Medical.System.Core.Models.Entities;
using Medical.System.Core.UnitOfWork;
using Medical.System.Core.Validator;
using System.Runtime.Versioning;

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

    public async Task<Supplier> GetById(string id)
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

    //public void Update(string id, Supplier supplierIn)
    //{
    //    _suppliers.ReplaceOne(supplier => supplier.Id == id, supplierIn);
    //}

    //public void Remove(string id)
    //{
    //    _suppliers.DeleteOne(supplier => supplier.Id == id);
    //}
}
