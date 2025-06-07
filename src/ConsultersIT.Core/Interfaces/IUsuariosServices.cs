using ConsultersIT.Common.Models.DTO;

namespace ConsultersIT.Core.Interfaces;

public interface IUsuariosServices
{
    Task<List<UsuarioDTO>> GetUsuarios();
    Task<string> AddUsuario(UsuarioDTO usuario);
    Task<LoginResponse> Login(LoginDTO login);
}