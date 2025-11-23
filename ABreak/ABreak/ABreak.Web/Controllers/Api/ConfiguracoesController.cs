using ABreak.Application.Interfaces;
using ABreak.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ABreak.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracoesController : ControllerBase
    {
        private readonly IConfiguracaoPausaService _configuracaoService;

        public ConfiguracoesController(IConfiguracaoPausaService configuracaoService)
        {
            _configuracaoService = configuracaoService;
        }

    
        [HttpGet]
        public async Task<ActionResult<object>> GetConfiguracoes(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string orderBy = "intervalominutos",
            [FromQuery] string order = "asc",
            [FromQuery] int? usuarioId = null,
            [FromQuery] bool? ativas = null)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1 || pageSize > 100) pageSize = 10;

            var todasConfiguracoes = await _configuracaoService.GetAllConfiguracoesAsync();
            var configuracoesFiltradas = todasConfiguracoes.AsQueryable();

            if (usuarioId.HasValue)
            {
                configuracoesFiltradas = configuracoesFiltradas.Where(c => c.UsuarioId == usuarioId.Value);
            }

            if (ativas.HasValue)
            {
                configuracoesFiltradas = configuracoesFiltradas.Where(c => c.Ativo == ativas.Value);
            }

            configuracoesFiltradas = orderBy.ToLower() switch
            {
                "intervalominutos" => order.ToLower() == "asc"
                    ? configuracoesFiltradas.OrderBy(c => c.IntervaloMinutos)
                    : configuracoesFiltradas.OrderByDescending(c => c.IntervaloMinutos),
                "tipopausanome" => order.ToLower() == "asc"
                    ? configuracoesFiltradas.OrderBy(c => c.TipoPausaNome)
                    : configuracoesFiltradas.OrderByDescending(c => c.TipoPausaNome),
                _ => configuracoesFiltradas.OrderBy(c => c.IntervaloMinutos)
            };

            var totalItems = configuracoesFiltradas.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var configuracoesPaginadas = configuracoesFiltradas
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var configuracoesComLinks = configuracoesPaginadas.Select(c => new
            {
                data = c,
                links = new[]
                {
                    new { rel = "self", href = Url.Action(nameof(GetConfiguracao), new { id = c.Id }), method = "GET" },
                    new { rel = "update", href = Url.Action(nameof(PutConfiguracao), new { id = c.Id }), method = "PUT" },
                    new { rel = "delete", href = Url.Action(nameof(DeleteConfiguracao), new { id = c.Id }), method = "DELETE" }
                }
            });

            var response = new
            {
                data = configuracoesComLinks,
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
                    new { rel = "self", href = Url.Action(nameof(GetConfiguracoes), new { pageNumber, pageSize, orderBy, order }), method = "GET" },
                    pageNumber > 1 ? new { rel = "previous", href = Url.Action(nameof(GetConfiguracoes), new { pageNumber = pageNumber - 1, pageSize, orderBy, order }), method = "GET" } : null,
                    pageNumber < totalPages ? new { rel = "next", href = Url.Action(nameof(GetConfiguracoes), new { pageNumber = pageNumber + 1, pageSize, orderBy, order }), method = "GET" } : null,
                    new { rel = "first", href = Url.Action(nameof(GetConfiguracoes), new { pageNumber = 1, pageSize, orderBy, order }), method = "GET" },
                    new { rel = "last", href = Url.Action(nameof(GetConfiguracoes), new { pageNumber = totalPages, pageSize, orderBy, order }), method = "GET" }
                }.Where(l => l != null)
            };

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetConfiguracao(int id)
        {
            var configuracao = await _configuracaoService.GetConfiguracaoByIdAsync(id);

            if (configuracao == null)
            {
                return NotFound(new { message = "Configuração não encontrada" });
            }

            var response = new
            {
                data = configuracao,
                links = new[]
                {
                    new { rel = "self", href = Url.Action(nameof(GetConfiguracao), new { id }), method = "GET" },
                    new { rel = "update", href = Url.Action(nameof(PutConfiguracao), new { id }), method = "PUT" },
                    new { rel = "delete", href = Url.Action(nameof(DeleteConfiguracao), new { id }), method = "DELETE" },
                    new { rel = "all", href = Url.Action(nameof(GetConfiguracoes)), method = "GET" }
                }
            };

            return Ok(response);
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<ActionResult<IEnumerable<ConfiguracaoPausaDTO>>> GetConfiguracoesByUsuario(int usuarioId)
        {
            var configuracoes = await _configuracaoService.GetConfiguracoesByUsuarioAsync(usuarioId);
            return Ok(configuracoes);
        }

        [HttpPost]
        public async Task<ActionResult<object>> PostConfiguracao(ConfiguracaoPausaDTO configuracaoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var configuracaoCriada = await _configuracaoService.CreateConfiguracaoAsync(configuracaoDto);

            var response = new
            {
                data = configuracaoCriada,
                links = new[]
                {
                    new { rel = "self", href = Url.Action(nameof(GetConfiguracao), new { id = configuracaoCriada.Id }), method = "GET" },
                    new { rel = "update", href = Url.Action(nameof(PutConfiguracao), new { id = configuracaoCriada.Id }), method = "PUT" },
                    new { rel = "delete", href = Url.Action(nameof(DeleteConfiguracao), new { id = configuracaoCriada.Id }), method = "DELETE" },
                    new { rel = "all", href = Url.Action(nameof(GetConfiguracoes)), method = "GET" }
                }
            };

            return CreatedAtAction(nameof(GetConfiguracao), new { id = configuracaoCriada.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutConfiguracao(int id, ConfiguracaoPausaDTO configuracaoDto)
        {
            if (id != configuracaoDto.Id)
            {
                return BadRequest(new { message = "ID não corresponde" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var configuracaoExistente = await _configuracaoService.GetConfiguracaoByIdAsync(id);
            if (configuracaoExistente == null)
            {
                return NotFound(new { message = "Configuração não encontrada" });
            }

            await _configuracaoService.UpdateConfiguracaoAsync(configuracaoDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfiguracao(int id)
        {
            var configuracao = await _configuracaoService.GetConfiguracaoByIdAsync(id);
            if (configuracao == null)
            {
                return NotFound(new { message = "Configuração não encontrada" });
            }

            await _configuracaoService.DeleteConfiguracaoAsync(id);

            return NoContent();
        }
    }
}