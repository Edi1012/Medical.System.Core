using Medical.System.Core.Models.Entities.Catalogs;

namespace Medical.System.Core.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}