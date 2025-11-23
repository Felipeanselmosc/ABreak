using ABreak.Application.Interfaces;
using ABreak.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ABreak.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
         
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            return Ok(usuarios);
        }
         
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);

            if (usuario == null)
            {
                return NotFound(new { message = "Usuário não encontrado" });
            }

            return Ok(usuario);
        }
         
        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> PostUsuario(UsuarioDTO usuarioDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuarioCriado = await _usuarioService.CreateUsuarioAsync(usuarioDto);

            return CreatedAtAction(
                nameof(GetUsuario),
                new { id = usuarioCriado.Id },
                usuarioCriado
            );
        }
         
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioDTO usuarioDto)
        {
            if (id != usuarioDto.Id)
            {
                return BadRequest(new { message = "ID não corresponde" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuarioExistente = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuarioExistente == null)
            {
                return NotFound(new { message = "Usuário não encontrado" });
            }

            await _usuarioService.UpdateUsuarioAsync(usuarioDto);

            return NoContent();
        }
         
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound(new { message = "Usuário não encontrado" });
            }

            await _usuarioService.DeleteUsuarioAsync(id);

            return NoContent();
        }
    }
}