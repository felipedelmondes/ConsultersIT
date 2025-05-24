using ConsultersIT.Common.Interfaces;
using ConsultersIT.Infra.Interfaces;
using ConsultersIT.Core.Interfaces;

namespace ConsultersIT.Core.Services;

public class TesteService : ITesteService
{
    private readonly ConsultersIT.Infra.Interfaces.ITesteRepository _testeRepository;

    public TesteService(ConsultersIT.Infra.Interfaces.ITesteRepository testeRepository)
    {
        _testeRepository = testeRepository;
    }

    public Task<string> GetTesteAsync()
    {
        return _testeRepository.GetTesteAsync();
    }
}