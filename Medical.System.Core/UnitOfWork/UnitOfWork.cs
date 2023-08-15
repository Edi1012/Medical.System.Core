using Medical.System.Core.Repositories.Interfaces;

namespace Medical.System.Core.UnitOfWork;


public class UnitOfWork : IUnitOfWork
{
    //public IRepository<User> Users { get; }
    public IUserRepository Users { get; }
    public ISupplierRepository Supplier { get; }

    public UnitOfWork(IUserRepository userRepository, ISupplierRepository supplier)
    {
        Users = userRepository;
        Supplier = supplier;
    }

    public Task CompleteAsync()
    {
        // En este caso, MongoDB maneja las transacciones automáticamente.
        // Pero puedes agregar lógica adicional si es necesario.
        return Task.CompletedTask;
    }
}

