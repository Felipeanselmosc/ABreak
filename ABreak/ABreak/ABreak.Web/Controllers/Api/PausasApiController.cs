using ABreak.Application.Interfaces;
using ABreak.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ABreak.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PausasController : ControllerBase
    {
        private readonly IPausaService _pausaService;
        private readonly ILogger<PausasController> _logger;

        public PausasController(IPausaService pausaService, ILogger<PausasController> logger)
        {
            _pausaService = pausaService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetPausas(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string orderBy = "DataHoraInicio",
            [FromQuery] string order = "desc",
            [FromQuery] int? usuarioId = null,
            [FromQuery] int? tipoPausaId = null,
            [FromQuery] bool? completada = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            var todasPausas = await _pausaService.GetAllPausasAsync();

            var pausasFiltradas = todasPausas.AsQueryable();

            if (usuarioId.HasValue)
            {
                pausasFiltradas = pausasFiltradas.Where(p => p.UsuarioId == usuarioId.Value);
            }

            if (tipoPausaId.HasValue)
            {
                pausasFiltradas = pausasFiltradas.Where(p => p.TipoPausaId == tipoPausaId.Value);
            }

            if (completada.HasValue)
            {
                pausasFiltradas = pausasFiltradas.Where(p => p.Completada == completada.Value);
            }

            pausasFiltradas = orderBy.ToLower() switch
            {
                "datahorainicio" => order.ToLower() == "asc"
                    ? pausasFiltradas.OrderBy(p => p.DataHoraInicio)
                    : pausasFiltradas.OrderByDescending(p => p.DataHoraInicio),
                "duracaominutos" => order.ToLower() == "asc"
                    ? pausasFiltradas.OrderBy(p => p.DuracaoMinutos)
                    : pausasFiltradas.OrderByDescending(p => p.DuracaoMinutos),
                "usuarionome" => order.ToLower() == "asc"
                    ? pausasFiltradas.OrderBy(p => p.UsuarioNome)
                    : pausasFiltradas.OrderByDescending(p => p.UsuarioNome),
                _ => pausasFiltradas.OrderByDescending(p => p.DataHoraInicio)
            };

            var totalItems = pausasFiltradas.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var pausasPaginadas = pausasFiltradas
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var pausasComLinks = pausasPaginadas.Select(p => new
            {
                data = p,
                links = new[]
                {
                    new { rel = "self", href = Url.Action(nameof(GetPausa), new { id = p.Id }), method = "GET" },
                    new { rel = "update", href = Url.Action(nameof(PutPausa), new { id = p.Id }), method = "PUT" },
                    new { rel = "delete", href = Url.Action(nameof(DeletePausa), new { id = p.Id }), method = "DELETE" }
                }
            });

            var response = new
            {
                data = pausasComLinks,
                pagination = new
                {
                    currentPage = pageNumber,
                    pageSize = pageSize,
                    totalItems = totalItems,
                    totalPages = totalPages,
                    hasPrevious = pageNumber > 1,
                    hasNext = pageNumber < totalPages
                },
                links = new[]
                {
                    new { rel = "self", href = Url.Action(nameof(GetPausas), new { pageNumber, pageSize, orderBy, order }), method = "GET" },
                    pageNumber > 1 ? new { rel = "previous", href = Url.Action(nameof(GetPausas), new { pageNumber = pageNumber - 1, pageSize, orderBy, order }), method = "GET" } : null,
                    pageNumber < totalPages ? new { rel = "next", href = Url.Action(nameof(GetPausas), new { pageNumber = pageNumber + 1, pageSize, orderBy, order }), method = "GET" } : null,
                    new { rel = "first", href = Url.Action(nameof(GetPausas), new { pageNumber = 1, pageSize, orderBy, order }), method = "GET" },
                    new { rel = "last", href = Url.Action(nameof(GetPausas), new { pageNumber = totalPages, pageSize, orderBy, order }), method = "GET" }
                }.Where(l => l != null)
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPausa(int id)
        {
            var pausa = await _pausaService.GetPausaByIdAsync(id);

            if (pausa == null)
            {
                return NotFound(new { message = "Pausa não encontrada" });
            }

            var response = new
            {
                data = pausa,
                links = new[]
                {
                    new { rel = "self", href = Url.Action(nameof(GetPausa), new { id }), method = "GET" },
                    new { rel = "update", href = Url.Action(nameof(PutPausa), new { id }), method = "PUT" },
                    new { rel = "delete", href = Url.Action(nameof(DeletePausa), new { id }), method = "DELETE" },
                    new { rel = "all", href = Url.Action(nameof(GetPausas)), method = "GET" }
                }
            };

            return Ok(response);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<PausaDTO>>> GetPausasByUsuario(int usuarioId)
        {
            var pausas = await _pausaService.GetPausasByUsuarioAsync(usuarioId);
            return Ok(pausas);
        }

        [HttpPost]
        public async Task<ActionResult<object>> PostPausa(PausaDTO pausaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pausaCriada = await _pausaService.CreatePausaAsync(pausaDto);

            var response = new
            {
                data = pausaCriada,
                links = new[]
                {
                    new { rel = "self", href = Url.Action(nameof(GetPausa), new { id = pausaCriada.Id }), method = "GET" },
                    new { rel = "update", href = Url.Action(nameof(PutPausa), new { id = pausaCriada.Id }), method = "PUT" },
                    new { rel = "delete", href = Url.Action(nameof(DeletePausa), new { id = pausaCriada.Id }), method = "DELETE" },
                    new { rel = "all", href = Url.Action(nameof(GetPausas)), method = "GET" }
                }
            };

            return CreatedAtAction(nameof(GetPausa), new { id = pausaCriada.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPausa(int id, PausaDTO pausaDto)
        {
            if (id != pausaDto.Id)
            {
                return BadRequest(new { message = "ID não corresponde" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pausaExistente = await _pausaService.GetPausaByIdAsync(id);
            if (pausaExistente == null)
            {
                return NotFound(new { message = "Pausa não encontrada" });
            }

            await _pausaService.UpdatePausaAsync(pausaDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePausa(int id)
        {
            var pausa = await _pausaService.GetPausaByIdAsync(id);
            if (pausa == null)
            {
                return NotFound(new { message = "Pausa não encontrada" });
            }

            await _pausaService.DeletePausaAsync(id);

            return NoContent();
        }
    }
}