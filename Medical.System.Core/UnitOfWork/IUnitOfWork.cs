using Medical.System.Core.Repositories.Interfaces;

namespace Medical.System.Core.UnitOfWork;


public interface IUnitOfWork
{
    //IRepository<User> Users { get; }
    IUserRepository Users { get; }
    ISupplierRepository Supplier { get; }
    Task CompleteAsync();
}

