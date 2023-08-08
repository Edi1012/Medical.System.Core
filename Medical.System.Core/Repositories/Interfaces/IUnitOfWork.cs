using Medical.System.Core.Models.Catalogs;

namespace Medical.System.Core.Repositories.Interfaces;


public interface IUnitOfWork
{
    IRepository<User> Users { get; }
    Task CompleteAsync();
}

