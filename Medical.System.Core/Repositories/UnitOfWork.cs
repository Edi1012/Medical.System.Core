
using Medical.System.Core.Models.Catalogs;
using Medical.System.Core.Repositories.Interfaces;

namespace Medical.System.Core.Repositories;


public class UnitOfWork : IUnitOfWork
{
    public IRepository<User> Users { get; }

    public UnitOfWork(IRepository<User> userRepository)
    {
        Users = userRepository;
    }

    public Task CompleteAsync()
    {
        // En este caso, MongoDB maneja las transacciones automáticamente.
        // Pero puedes agregar lógica adicional si es necesario.
        return Task.CompletedTask;
    }
}

