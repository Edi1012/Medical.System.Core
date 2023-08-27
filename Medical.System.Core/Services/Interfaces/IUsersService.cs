using Medical.System.Core.Models.DTOs;
using Medical.System.Core.Models.Entities;

namespace Medical.System.Core.Services.Interfaces;

public interface IUsersService
{
    Task<User> CreateUserAsync(CreateUserDto user);
    Task<User> GetUserAsync(string id);
    Task<IEnumerable<User>> GetAll();
}