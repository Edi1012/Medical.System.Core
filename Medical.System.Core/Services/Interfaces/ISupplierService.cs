using Medical.System.Core.Models.DTOs;
using Medical.System.Core.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Medical.System.Core.Services.Implementations
{
    public interface ISupplierService
    {
        Task<Supplier> Create(CreateSupplierDto supplier);
        Task<Supplier> GetByIdAsync(string id);
    }
}