using ABreak.Application.Interfaces;
using ABreak.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ABreak.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposPausaController : ControllerBase
    {
        private readonly ITipoPausaService _tipoPausaService;

        public TiposPausaController(ITipoPausaService tipoPausaService)
        {
            _tipoPausaService = tipoPausaService;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetTiposPausa(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string orderBy = "nome",
            [FromQuery] string order = "asc",
            [FromQuery] bool? ativos = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            var todosTipos = await _tipoPausaService.GetAllTiposPausaAsync();
            var tiposFiltrados = todosTipos.AsQueryable();

            if (ativos.HasValue)
            {
                tiposFiltrados = tiposFiltrados.Where(t => t.Ativo == ativos.Value);
            }

            tiposFiltrados = orderBy.ToLower() switch
            {
                "nome" => order.ToLower() == "asc"
                    ? tiposFiltrados.OrderBy(t => t.Nome)
                    : tiposFiltrados.OrderByDescending(t => t.Nome),
                "duracaorecomendadaminutos" => order.ToLower() == "asc"
                    ? tiposFiltrados.OrderBy(t => t.DuracaoRecomendadaMinutos)
                    : tiposFiltrados.OrderByDescending(t => t.DuracaoRecomendadaMinutos),
                _ => tiposFiltrados.OrderBy(t => t.Nome)
            };

            var totalItems = tiposFiltrados.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var tiposPaginados = tiposFiltrados
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var tiposComLinks = tiposPaginados.Select(t => new
            {
                data = t,
                links = new[]
                {
                    new { rel = "self", href = Url.Action(nameof(GetTipoPausa), new { id = t.Id }), method = "GET" },
                    new { rel = "update", href = Url.Action(nameof(PutTipoPausa), new { id = t.Id }), method = "PUT" },
                    new { rel = "delete", href = Url.Action(nameof(DeleteTipoPausa), new { id = t.Id }), method = "DELETE" }
                }
            });

            var response = new
            {
                data = tiposComLinks,
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
                    new { rel = "self", href = Url.Action(nameof(GetTiposPausa), new { pageNumber, pageSize, orderBy, order }), method = "GET" },
                    pageNumber > 1 ? new { rel = "previous", href = Url.Action(nameof(GetTiposPausa), new { pageNumber = pageNumber - 1, pageSize, orderBy, order }), method = "GET" } : null,
                    pageNumber < totalPages ? new { rel = "next", href = Url.Action(nameof(GetTiposPausa), new { pageNumber = pageNumber + 1, pageSize, orderBy, order }), method = "GET" } : null,
                    new { rel = "first", href = Url.Action(nameof(GetTiposPausa), new { pageNumber = 1, pageSize, orderBy, order }), method = "GET" },
                    new { rel = "last", href = Url.Action(nameof(GetTiposPausa), new { pageNumber = totalPages, pageSize, orderBy, order }), method = "GET" }
                }.Where(l => l != null)
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetTipoPausa(int id)
        {
            var tipoPausa = await _tipoPausaService.GetTipoPausaByIdAsync(id);

            if (tipoPausa == null)
            {
                return NotFound(new { message = "Tipo de pausa não encontrado" });
            }

            var response = new
            {
                data = tipoPausa,
                links = new[]
                {
                    new { rel = "self", href = Url.Action(nameof(GetTipoPausa), new { id }), method = "GET" },
                    new { rel = "update", href = Url.Action(nameof(PutTipoPausa), new { id }), method = "PUT" },
                    new { rel = "delete", href = Url.Action(nameof(DeleteTipoPausa), new { id }), method = "DELETE" },
                    new { rel = "all", href = Url.Action(nameof(GetTiposPausa)), method = "GET" }
                }
            };

            return Ok(response);
        }
         
        [HttpGet("ativos")]
        public async Task<ActionResult<IEnumerable<TipoPausaDTO>>> GetTiposPausaAtivos()
        {
            var tipos = await _tipoPausaService.GetTiposPausaAtivosAsync();
            return Ok(tipos);
        }
         
        [HttpPost]
        public async Task<ActionResult<object>> PostTipoPausa(TipoPausaDTO tipoPausaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tipoCriado = await _tipoPausaService.CreateTipoPausaAsync(tipoPausaDto);

            var response = new
            {
                data = tipoCriado,
                links = new[]
                {
                    new { rel = "self", href = Url.Action(nameof(GetTipoPausa), new { id = tipoCriado.Id }), method = "GET" },
                    new { rel = "update", href = Url.Action(nameof(PutTipoPausa), new { id = tipoCriado.Id }), method = "PUT" },
                    new { rel = "delete", href = Url.Action(nameof(DeleteTipoPausa), new { id = tipoCriado.Id }), method = "DELETE" },
                    new { rel = "all", href = Url.Action(nameof(GetTiposPausa)), method = "GET" }
                }
            };

            return CreatedAtAction(nameof(GetTipoPausa), new { id = tipoCriado.Id }, response);
        }
         
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipoPausa(int id, TipoPausaDTO tipoPausaDto)
        {
            if (id != tipoPausaDto.Id)
            {
                return BadRequest(new { message = "ID não corresponde" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var tipoExistente = await _tipoPausaService.GetTipoPausaByIdAsync(id);
            if (tipoExistente == null)
            {
                return NotFound(new { message = "Tipo de pausa não encontrado" });
            }

            await _tipoPausaService.UpdateTipoPausaAsync(tipoPausaDto);

            return NoContent();
        }
         
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoPausa(int id)
        {
            var tipoPausa = await _tipoPausaService.GetTipoPausaByIdAsync(id);
            if (tipoPausa == null)
            {
                return NotFound(new { message = "Tipo de pausa não encontrado" });
            }

            await _tipoPausaService.DeleteTipoPausaAsync(id);

            return NoContent();
        }
    }
}