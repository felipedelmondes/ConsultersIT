using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ConsultersIT.Core.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace ConsultersIT.Core.Services;

public class JwtService : IJwtService
{
    public string GenerateToken(string username, int expireMinutes = 60)
    {
        var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? "S16D/psC/ljNyi3zEGq9GpgILIwwPi+mnIXYPtJ7nUI=";
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
            Expires = DateTime.UtcNow.AddMinutes(expireMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}