using Medical.System.Core.Models.Catalogs;

namespace Medical.System.Core.Services.Interfaces;

public interface ICatalogsService
{
    Task CreateUserAsync(User user);
    Task DeleteUserAsync(string id);
    Task<User> GetUserByIdAsync(string id);
    Task UpdateUserAsync(User user);
    Task<bool> ExistUserNameAsync(string userName);
}