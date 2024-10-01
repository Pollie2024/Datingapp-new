using System.Security.Claims;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService(IConfiguration config) : ITokenService
{
    string ITokenService.CreateToken(AppUser user)
    {
        var tokenKey = System.Text.Encoding.UTF8.GetBytes(config["TokenKey"] ?? throw new InvalidOperationException("Cannot access tokenKey from appsettings"));
        if (tokenKey.Length < 64) throw new Exception("TokenKey must be at least 64 bytes long");
        
        var key = new SymmetricSecurityKey(tokenKey);
    
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserName)
        };
    
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
    
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = creds
        };
    
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
    
        return tokenHandler.WriteToken(token);
    }   
}

internal class JwtSecurityTokenHandler
{
    public JwtSecurityTokenHandler()
    {
    }

    internal object CreateToken(SecurityTokenDescriptor tokenDescriptor)
    {
        throw new NotImplementedException();
    }

    internal string WriteToken(object token)
    {
        throw new NotImplementedException();
    }
}