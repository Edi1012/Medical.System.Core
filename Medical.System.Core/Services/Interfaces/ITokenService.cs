using Medical.System.Core.Models.Entities;

namespace Medical.System.Core.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
        bool ValidateToken(string token);
    }
}