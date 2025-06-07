using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsultersIT.Common.Models.DTO;

namespace ConsultersIT.Infra.Interfaces
{
    public interface IUsuarioRepository
    {
        Task <List<UsuarioDTO>> GetUsuarios();
        Task<string> AddUsuario(UsuarioDTO usuario);
        Task<LoginResponse> Login(LoginDTO login);
    }
}