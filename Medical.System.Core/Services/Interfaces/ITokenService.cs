using DnsClient;
using Medical.System.Core.Models.DTOs;
using Medical.System.Core.Models.Entities;

namespace Medical.System.Core.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(LoginDTO user);
        //bool ValidateToken(string token);
    }
}