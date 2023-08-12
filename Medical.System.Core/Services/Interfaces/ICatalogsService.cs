using Medical.System.Core.Models.DTOs;
using Medical.System.Core.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Medical.System.Core.Services.Interfaces;

public interface ICatalogsService
{
    Task<User> CreateUserAsync(CreateUserDto user);
}