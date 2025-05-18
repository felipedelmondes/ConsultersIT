using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsultersIT.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConsultersIT.API.Controllers
{
    [ApiController]
    public class TesteController : ControllerBase
    {
        private readonly ITesteService _service;

        public TesteController(ITesteService service)
        {
            _service = service;
        }


        [HttpGet]      
        [Route("api/TesteDB")]
        public Task<string> Get()
        {
            return _service.GetTesteAsync();
        }
    }
}