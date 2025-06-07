using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultersIT.Common.Models.DTO
{
    public class UsuarioDTO
    {
        public string nome_usuario { get; set; }
        public string senha_hash { get; set; }
        public string email { get; set; }
        
    }
}