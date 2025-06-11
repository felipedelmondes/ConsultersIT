using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsultersIT.Common.Models.DTO;
using ConsultersIT.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConsultersIT.API.Controllers
{
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuariosServices _usuariosServices;

        public UsuarioController(IUsuariosServices usuariosServices)
        {
            _usuariosServices = usuariosServices;
        }

        [Authorize]
        [HttpGet]
        [Route("api/GetUsuarios")]
        public Task<List<UsuarioDTO>> GetUsuarios()
        {
            return _usuariosServices.GetUsuarios();
        }
        
        [Authorize]
        [HttpPost]
        [Route("api/AddUsuario")]
        public async Task<string> AddUsuario([FromBody] UsuarioDTO usuario)
        {
            if (usuario.nome_usuario.Trim() == null || usuario.senha_hash.Trim() == null)
            {
                return "Verifique os dados informados. Nome e senha não podem ser nulos.";
            }
            return await _usuariosServices.AddUsuario(usuario);
        }
        
        [HttpPost]
        [Route("api/Login")]
        public async Task<LoginResponse> Login(LoginDTO login)
        {
            if (login.username.Trim() == null || login.senha.Trim() == null)
            {
                return new LoginResponse { Mensagem = "Verifique os dados informados. Nome e senha não podem ser nulos." };
            }
            return await _usuariosServices.Login(login);
        }
        
    }
}