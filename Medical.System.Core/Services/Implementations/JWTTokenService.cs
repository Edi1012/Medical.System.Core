using Medical.System.Core.Models.Entities.Catalogs;
using Medical.System.Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Medical.System.Core.Services.Implementations;

public class TokenService : ITokenService
{

    public TokenService(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public string CreateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                //new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Country, "Mexico"),
                new Claim(ClaimTypes.StreetAddress, "Rio Culiacan"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique Token identifier
                // Aquí puedes agregar más reclamos si los necesitas
            }),
            Expires = DateTime.UtcNow.AddHours(1), // Expiración en 1 hora
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    //public string GenerateJwtToken(string username)
    //{
    //    var claims = new[]
    //    {
    //    new Claim(JwtRegisteredClaimNames.Sub, username),
    //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique Token identifier
    //    // You can add more claims here if needed
    //};

    //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
    //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //    var token = new JwtSecurityToken(
    //        issuer: Configuration["Jwt:Issuer"],
    //        audience: Configuration["Jwt:Audience"],
    //        claims: claims,
    //        expires: DateTime.Now.AddMinutes(30), // You can set the token expiration time
    //        signingCredentials: creds);

    //    return new JwtSecurityTokenHandler().WriteToken(token);
    //}
}
