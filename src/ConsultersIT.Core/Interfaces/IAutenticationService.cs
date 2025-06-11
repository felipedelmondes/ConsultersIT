namespace ConsultersIT.Core.Interfaces;

public interface IAutenticationService
{
    public string EncryptPassword(string password);
}