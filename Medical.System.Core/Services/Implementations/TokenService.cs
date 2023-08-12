using FluentValidation;
using Medical.System.Core.Models.DTOs;
using Medical.System.Core.Models.Entities;
using Medical.System.Core.Repositories;
using Medical.System.Core.Repositories.Interfaces;
using Medical.System.Core.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Medical.System.Core.Services.Implementations;

public class TokenService : ITokenService
{

    public TokenService(IConfiguration configuration, IUserRepository userRepository)
    {
        Configuration = configuration;
        UserRepository = userRepository;
    }

    public IConfiguration Configuration { get; }
    public IUserRepository UserRepository { get; }

    //public string CreateToken(User user)
    //{
    //    var tokenHandler = new JwtSecurityTokenHandler();
    //    var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Key"]);
    //    var tokenDescriptor = new SecurityTokenDescriptor
    //    {
    //        Subject = new ClaimsIdentity(new Claim[]
    //        {
    //            //new Claim(ClaimTypes.Name, user.Email),
    //            new Claim(ClaimTypes.Country, "Mexico"),
    //            new Claim(ClaimTypes.StreetAddress, "Rio Culiacan"),
    //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique Token identifier
    //            // Aquí puedes agregar más reclamos si los necesitas
    //        }),
    //        Expires = DateTime.UtcNow.AddHours(1), // Expiración en 1 hora
    //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //    };
    //    var token = tokenHandler.CreateToken(tokenDescriptor);
    //    return tokenHandler.WriteToken(token);
    //}

    public async Task<string> CreateToken(LoginDTO loginDTO)
    {


        if (!await UserRepository.ExistUserNameAsync(loginDTO.Username))
        {
            throw new ValidationException("Usuario no Existe");
        }

        if (!await UserRepository.Loggin(loginDTO))
        {
            throw new ValidationException("Usuario no Existe");
        }

        User user = new User() { Login = new Login() { Username = "EdgarIsaac", PasswordHash = "Hola1" } };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            // Aquí puedes añadir claims adicionales como los roles del usuario
            new Claim(JwtRegisteredClaimNames.Sub, user.Login.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, "Admin"),

        };

        var token = new JwtSecurityToken(
            issuer: Configuration["Jwt:Issuer"],
            audience: Configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30), // Expiración, ajustable según tus necesidades
            signingCredentials: creds);


        
        UserRepository.UpdateTokenAsync(new Login() { Username = loginDTO.Username, Token = new JwtSecurityTokenHandler().WriteToken(token) });


        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public bool ValidateToken(string token)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            Console.WriteLine($"Issuer: {Configuration["Jwt:Issuer"]}");
            Console.WriteLine($"Audience: {Configuration["Jwt:Audience"]}");

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = Configuration["Jwt:Issuer"],
                ValidAudience = Configuration["Jwt:Audience"],
                IssuerSigningKey = key,
                RequireAudience = false,
            }, out _);

            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
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
