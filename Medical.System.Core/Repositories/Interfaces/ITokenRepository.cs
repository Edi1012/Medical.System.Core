using Medical.System.Core.Models.Entities;

namespace Medical.System.Core.Repositories.Interfaces;

public interface ITokenRepository : IGenericRepository<RevokedToken>
{
    Task<RevokedToken> GetByUserIdAsync(Guid UserId);
}