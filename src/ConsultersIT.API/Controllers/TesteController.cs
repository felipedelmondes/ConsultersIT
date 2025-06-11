using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsultersIT.Common.Interfaces;
using ConsultersIT.Common.Models.DTO;
using ConsultersIT.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConsultersIT.API.Controllers
{
    [ApiController]
    public class TesteController : ControllerBase
    {
        public TesteController(ITesteService service, ConsultersIT.Common.Interfaces.ICepResponse cepService)
        {
            _service = service;
            _cepService = cepService;
        }

        private readonly ITesteService _service;
        private readonly ConsultersIT.Common.Interfaces.ICepResponse _cepService;
        

        [Authorize]
        [HttpGet]
        [Route("api/TesteDB")]
        public Task<string> Get()
        {
            return _service.GetTesteAsync();
        }
        
        [HttpPost]
        [Route("api/TesteCep")]
        public async Task<string> GetCep([FromForm] string cep)
        {
            var resultado = await _cepService.ConsultarCepAsync(cep);
            if (resultado == null)
                return "Erro ao consultar o CEP.";
            var retorno = $"Teste com sucesso. Cep: {resultado.Cep} Logradouro: {resultado.Logradouro} Bairro: {resultado.Bairro} Cidade: {resultado.Localidade} UF: {resultado.Uf}";
            return retorno;
        }
    }
}
