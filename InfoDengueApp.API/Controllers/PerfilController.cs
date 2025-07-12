using InfoDengueApp.Application.DTOs.Request;
using InfoDengueApp.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InfoDengueApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilService _perfilService;

        public PerfilController(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        // GET: api/perfil
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var perfis = await _perfilService.ListarAsync();
            return Ok(perfis);
        }

        // GET: api/perfil/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var perfil = await _perfilService.ObterPorIdAsync(id);
            return Ok(perfil);
        }

        // POST: api/perfil
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] PerfilRequest request)
        {
            var novoPerfil = await _perfilService.CriarAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = novoPerfil.Id }, novoPerfil);
        }

        // PUT: api/perfil/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PerfilRequest request)
        {
            await _perfilService.AtualizarAsync(id, request);
            return NoContent();
        }

        // DELETE: api/perfil/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _perfilService.ExcluirAsync(id);
            return NoContent();
        }
    }
}
