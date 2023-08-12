using Medical.System.Core.Models.DTOs;
using Medical.System.Core.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Medical.System.Core.Services.Interfaces;

public interface IUsersService
{
    Task<User> CreateUserAsync(CreateUserDto user);
}