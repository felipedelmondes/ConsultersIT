using ConsultersIT.Common.Models.DTO;

namespace ConsultersIT.Common.Interfaces;

public interface ICepResponse
{
    Task<CepResponse?> ConsultarCepAsync(string cep);
}