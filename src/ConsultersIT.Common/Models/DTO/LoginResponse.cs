namespace ConsultersIT.Common.Models.DTO;

public class LoginResponse
{
    public string Mensagem { get; set; }
    public string Jwt { get; set; }
    public string Hash { get; set; }
    public string Expire { get; set; } 
}