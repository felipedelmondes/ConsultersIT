using ConsultersIT.Common.Models.DTO;
using ConsultersIT.Core.Interfaces;
using ConsultersIT.Infra.Interfaces;

namespace ConsultersIT.Core.Services;

public class UsuariosService : IUsuariosServices
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IAutenticationService _autenticationService;
    private readonly IJwtService _jwtService;

    public UsuariosService(IUsuarioRepository usuarioRepository, IAutenticationService autenticationService, IJwtService jwtService)
    {
        _usuarioRepository = usuarioRepository;
        _autenticationService = autenticationService;
        _jwtService = jwtService;

    }

    public Task<List<UsuarioDTO>> GetUsuarios()
    {
        return _usuarioRepository.GetUsuarios();
    }

    public Task<string> AddUsuario(UsuarioDTO usuario)
    {
        usuario.senha_hash = _autenticationService.EncryptPassword(usuario.senha_hash);
        return _usuarioRepository.AddUsuario(usuario);
    }

    public async Task<LoginResponse> Login(LoginDTO login)
    {
        login.senha = _autenticationService.EncryptPassword(login.senha);
        var autenticacao = await _usuarioRepository.Login(login);
        if (autenticacao != null)
        {
            autenticacao.Jwt = _jwtService.GenerateToken(autenticacao.Hash);
            autenticacao.Expire = "60 min";
        }
        return autenticacao;
    }
}