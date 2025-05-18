using ConsultersIT.Core.Interfaces;
using ConsultersIT.Infra.Interfaces;

namespace ConsultersIT.Core.Services;

public class TesteService : ITesteService
{
    public readonly ITesteRepository _Respository;

    public TesteService(ITesteRepository respository)
    {
        _Respository = respository;
    }

    public Task<string> GetTesteAsync()
    {
        return _Respository.GetTesteAsync();
    }
}