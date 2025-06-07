using ConsultersIT.Common.Models.DTO;
using ConsultersIT.Core.Interfaces;
using ConsultersIT.Infra.Interfaces;

namespace ConsultersIT.Core.Services;

public class UsuariosService : IUsuariosServices
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IAutenticationService _autenticationService;

    public UsuariosService(IUsuarioRepository usuarioRepository, IAutenticationService autenticationService)
    {
        _usuarioRepository = usuarioRepository;
        _autenticationService = autenticationService;

    }

    public Task<List<UsuarioDTO>> GetUsuarios()
    {
        return _usuarioRepository.GetUsuarios();
    }

    public Task<string> AddUsuario(UsuarioDTO usuario)
    {
        usuario.senha_hash = _autenticationService.HashPassword(usuario.senha_hash);
        return _usuarioRepository.AddUsuario(usuario);
    }

    public Task<LoginResponse> Login(LoginDTO login)
    {
        return _usuarioRepository.Login(login);
    }
}