using Medical.System.Core.Models.DTOs;
using Medical.System.Core.Models.Entities;

namespace Medical.System.Core.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<bool> ExistUserNameAsync(string userName);
    Task<bool> Loggin(LoginDTO loggingDTO);
    Task<bool> UpdateTokenAsync(Login login);
}