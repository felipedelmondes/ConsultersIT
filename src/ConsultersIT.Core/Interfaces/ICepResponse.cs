using ConsultersIT.Core.Models.DTO;

namespace ConsultersIT.Core.Interfaces;

public interface ICepResponse
{
    Task<CepResponse?> ConsultarCepAsync(string cep);
}