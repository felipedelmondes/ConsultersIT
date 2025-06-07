using BCrypt.Net;
using ConsultersIT.Core.Interfaces;
using Org.BouncyCastle.Crypto.Generators;

namespace ConsultersIT.Core.Services;

public class AutenticationService : IAutenticationService
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}