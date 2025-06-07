namespace ConsultersIT.Core.Interfaces;

public interface IJwtService
{
    public string GenerateToken(string username, string senha, int expireMinutes = 60);
}