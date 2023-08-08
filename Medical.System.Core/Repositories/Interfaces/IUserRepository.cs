using Medical.System.Core.Models.Entities.Catalogs;

namespace Medical.System.Core.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<long> CountUsersAsync();
}