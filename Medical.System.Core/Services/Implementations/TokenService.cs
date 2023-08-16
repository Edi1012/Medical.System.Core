
using Medical.System.Core.Exceptions;
using Medical.System.Core.Models.DTOs;
using Medical.System.Core.Models.Entities;
using Medical.System.Core.Repositories;
using Medical.System.Core.Repositories.Implementations;
using Medical.System.Core.Repositories.Interfaces;
using Medical.System.Core.Services.Interfaces;
using Medical.System.Core.Validator;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Medical.System.Core.Services.Implementations;

public class TokenService : ITokenService
{

    public TokenService(IConfiguration configuration, IUserRepository userRepository, ITokenRepository tokenRepository)
    {
        Configuration = configuration;
        TokenRepository = tokenRepository;
        UserRepository = userRepository;
    }


    public IConfiguration Configuration { get; }
    public ITokenRepository TokenRepository { get; }
    public IUserRepository UserRepository { get; }

    public async Task<string> CreateToken(LoginDTO loginDTO)
    {

        var validator = new CreateTokenValidator();
        var result = validator.Validate(loginDTO);

        if (!result.IsValid)
        {
            throw new FluentValidation.ValidationException(result.Errors);
        }

        User user = await UserRepository.GetByLogginAsync(new Loggin() { Username = loginDTO.Username, PasswordHash = loginDTO.PasswordHash });


        if (user == null) 
        {
            throw new NotFoundException("Usuario no encontrado.");
        }

        //get token from DB
        var RevokedToken  = await TokenRepository.GetByUserIdAsync(user.Id);

        if (RevokedToken != null) 
        {
            if(!RevokedToken.Revoked)
                if(this.ValidateToken(RevokedToken.TokenID))
                    return RevokedToken.TokenID;
        }


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            // Aquí puedes añadir claims adicionales como los roles del usuario
            new Claim(JwtRegisteredClaimNames.Sub, user.Login.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, "Admin"),

        };


        // Añadir un claim por cada rol del usuario
        foreach (var role in user.Roles)
        {
            claims.Append(new Claim(ClaimTypes.Role, role.Name));
        }


        var jwtSecurityToken = new JwtSecurityToken(
            issuer: Configuration["Jwt:Issuer"],
            audience: Configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30), // Expiración, ajustable según tus necesidades
            signingCredentials: creds);

        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        // Crear el objeto RevokedToken y llenar los campos necesarios
        var revokedToken = new RevokedToken
        {
            TokenID = token,
            UserID = user.Id, // Asegúrate de que user.Id tenga el ObjectId correcto
            ValidFrom = jwtSecurityToken.ValidFrom,
            ValidTo = jwtSecurityToken.ValidTo,
            Revoked = false
        };

        
        await TokenRepository.AddAsync(revokedToken);
        //UserRepository.UpdateTokenAsync(new Loggin() { Username = loginDTO.Username, Token = new JwtSecurityTokenHandler().WriteToken(token) });


        return token;
    }
    private bool ValidateToken(string token)
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
        catch (Exception ex)
        {
            return false;
        }
    }

    //TODO: add token repository and add token to database and to user
    public static string EncryptToken(string token, string key, string iv)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Convert.FromBase64String(key); // Clave de 256 bits
            aesAlg.IV = Convert.FromBase64String(iv);  // Vector de inicialización de 128 bits

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(token);
                    }
                }
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

}
