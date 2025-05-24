using ConsultersIT.Common.Interfaces;
using ConsultersIT.Common.Models.DTO;

namespace ConsultersIT.Core.Services;

using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

public class CepService : ICepResponse
{
    public async Task<CepResponse?> ConsultarCepAsync(string cep)
    {
        var retorno = new CepResponse();
        try
        {
            using var httpClient = new HttpClient();
            var url = $"https://viacep.com.br/ws/{cep}/json/";
            var response = await httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
                return null;
            var json = await response.Content.ReadAsStringAsync();
            retorno = JsonSerializer.Deserialize<CepResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return retorno;
        }
        catch (HttpRequestException)
        {
            // Logar ou tratar erro de requisição HTTP
            return null;
        }
        catch (TaskCanceledException)
        {
            // Logar ou tratar timeout
            return null;
        }
        catch (JsonException)
        {
            // Logar ou tratar erro de deserialização
            return null;
        }
        catch (Exception)
        {
            // Logar ou tratar erro genérico
            return null;
        }
    }
}

