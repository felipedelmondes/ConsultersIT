using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsultersIT.Common.Models.DTO;
using ConsultersIT.Infra.Data.Context;
using ConsultersIT.Infra.Interfaces;
using Dapper;

namespace ConsultersIT.Infra.Repositories
{
    public class UsuarioRepository : BaseRepository, IUsuarioRepository
    {
        public UsuarioRepository(IDbConnectionFactory connectionFactory) 
            : base(connectionFactory) { }

        public async Task<List<UsuarioDTO>> GetUsuarios()
        {
            var query = @"select 
                    u.nome_usuario,
                    u.email,
                    u.senha_hash
                from usuarios u";
            try
            {
                return await WithConnection<List<UsuarioDTO>>(async conn =>
                    (await conn.QueryAsync<UsuarioDTO>(query)).ToList()
                );
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return new List<UsuarioDTO>();
            }
        }
        
        public async Task<string> AddUsuario(UsuarioDTO usuario)
        {
            var query = @"INSERT INTO usuarios (nome_usuario, email, senha_hash) 
                          VALUES (@NomeUsuario, @Email, @SenhaHash)";
            try
            {
                return await WithConnection<string>(async conn =>
                {
                    var result = await conn.ExecuteAsync(query, new
                    {
                        NomeUsuario = usuario.nome_usuario.ToUpper().Trim(),
                        Email = usuario.email.ToUpper().Trim(),
                        SenhaHash = usuario.senha_hash.Trim()
                    });
                    return "Usuario adicionado com sucesso !!!! ";
                });
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return "Erro ao adicionar usuario: " + ex.Message;
            }
        }

        public async Task<LoginResponse> Login(LoginDTO login)
        {
            var query = @"select 
                        u.nome_usuario,
                        u.senha_hash
                    from usuarios u
                    where u.nome_usuario = @NomeUsuario";

            try
            {
                return await WithConnection<LoginResponse>(async conn =>
                {
                    var usuario = await conn.QueryFirstOrDefaultAsync<LoginDTO>(query, new
                    {
                        NomeUsuario = login.username.Trim()
                    });

                    if (usuario == null)
                    {
                        return new LoginResponse { Mensagem = "Usuário ou senha inválidos." };
                    }

                    // Retorna o hash para ser comparado na camada de serviço
                    return new LoginResponse()
                    {
                        Mensagem = "Login realizado com sucesso",
                        Hash = usuario.username // Adicione esta propriedade se necessário
                    };
                });
            }
            catch (Exception ex)
            {
                return new LoginResponse()
                {
                    Mensagem = "Erro ao realizar login: " + ex.Message
                };
            }
        }
    }
}