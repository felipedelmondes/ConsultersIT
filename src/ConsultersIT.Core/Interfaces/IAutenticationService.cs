namespace ConsultersIT.Core.Interfaces;

public interface IAutenticationService
{
    public string HashPassword(string password);
    public bool VerifyPassword(string password, string hashedPassword);
}