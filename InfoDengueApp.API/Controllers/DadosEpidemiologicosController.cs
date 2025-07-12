using InfoDengueApp.Application.DTOs.External;
using InfoDengueApp.Application.Interfaces;
using InfoDengueApp.Application.Interfaces.ExternalServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InfoDengueApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // exige token JWT para acessar
    public class DadosEpidemiologicosController : ControllerBase
    {
        private readonly IDadosEpidemiologicosService _dadosService;

        public DadosEpidemiologicosController(IDadosEpidemiologicosService dadosService)
        {
            _dadosService = dadosService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDados([FromQuery] DadosEpidemiologicosFiltroRequest filtro)
        {
            var resultado = await _dadosService.ObterDadosEpidemiologicosAsync(filtro);
            return Ok(resultado);
        }
    }
}
