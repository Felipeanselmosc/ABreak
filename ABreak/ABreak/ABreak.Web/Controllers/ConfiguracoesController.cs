using ABreak.Application.Interfaces;
using ABreak.Application.ViewModels;
using ABreak.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ABreak.Web.Controllers
{
    public class ConfiguracoesController : Controller
    {
        private readonly IConfiguracaoPausaService _configuracaoService;
        private readonly IUsuarioService _usuarioService;
        private readonly ITipoPausaService _tipoPausaService;

        public ConfiguracoesController(
            IConfiguracaoPausaService configuracaoService,
            IUsuarioService usuarioService,
            ITipoPausaService tipoPausaService)
        {
            _configuracaoService = configuracaoService;
            _usuarioService = usuarioService;
            _tipoPausaService = tipoPausaService;
        }
         
        public async Task<IActionResult> Index()
        {
            var configuracoes = await _configuracaoService.GetAllConfiguracoesAsync();
            return View(configuracoes);
        }
         
        public async Task<IActionResult> Details(int id)
        {
            var configuracao = await _configuracaoService.GetConfiguracaoByIdAsync(id);
            if (configuracao == null)
            {
                return NotFound();
            }
            return View(configuracao);
        }
         
        public async Task<IActionResult> Create()
        {
            await CarregarSelectLists();
            var model = new ConfiguracaoPausaViewModel
            {
                IntervaloMinutos = 60,
                HorarioInicioTrabalho = new TimeSpan(8, 0, 0),
                HorarioFimTrabalho = new TimeSpan(18, 0, 0),
                NotificacaoAtiva = true,
                Ativo = true
            };
            return View(model);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConfiguracaoPausaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var configuracaoDto = new ConfiguracaoPausaDTO
                {
                    UsuarioId = model.UsuarioId,
                    TipoPausaId = model.TipoPausaId,
                    IntervaloMinutos = model.IntervaloMinutos,
                    HorarioInicioTrabalho = model.HorarioInicioTrabalho,
                    HorarioFimTrabalho = model.HorarioFimTrabalho,
                    NotificacaoAtiva = model.NotificacaoAtiva,
                    Ativo = true
                };

                await _configuracaoService.CreateConfiguracaoAsync(configuracaoDto);
                TempData["SuccessMessage"] = "Configuração criada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            await CarregarSelectLists();
            return View(model);
        }
         
        public async Task<IActionResult> Edit(int id)
        {
            var configuracao = await _configuracaoService.GetConfiguracaoByIdAsync(id);
            if (configuracao == null)
            {
                return NotFound();
            }

            var model = new ConfiguracaoPausaViewModel
            {
                Id = configuracao.Id,
                UsuarioId = configuracao.UsuarioId,
                TipoPausaId = configuracao.TipoPausaId,
                IntervaloMinutos = configuracao.IntervaloMinutos,
                HorarioInicioTrabalho = configuracao.HorarioInicioTrabalho,
                HorarioFimTrabalho = configuracao.HorarioFimTrabalho,
                NotificacaoAtiva = configuracao.NotificacaoAtiva,
                Ativo = configuracao.Ativo
            };

            await CarregarSelectLists();
            return View(model);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ConfiguracaoPausaViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var configuracaoDto = new ConfiguracaoPausaDTO
                {
                    Id = model.Id,
                    UsuarioId = model.UsuarioId,
                    TipoPausaId = model.TipoPausaId,
                    IntervaloMinutos = model.IntervaloMinutos,
                    HorarioInicioTrabalho = model.HorarioInicioTrabalho,
                    HorarioFimTrabalho = model.HorarioFimTrabalho,
                    NotificacaoAtiva = model.NotificacaoAtiva,
                    Ativo = model.Ativo
                };

                await _configuracaoService.UpdateConfiguracaoAsync(configuracaoDto);
                TempData["SuccessMessage"] = "Configuração atualizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            await CarregarSelectLists();
            return View(model);
        }
         
        public async Task<IActionResult> Delete(int id)
        {
            var configuracao = await _configuracaoService.GetConfiguracaoByIdAsync(id);
            if (configuracao == null)
            {
                return NotFound();
            }
            return View(configuracao);
        }
         
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _configuracaoService.DeleteConfiguracaoAsync(id);
            TempData["SuccessMessage"] = "Configuração excluída com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private async Task CarregarSelectLists()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            var tiposPausa = await _tipoPausaService.GetTiposPausaAtivosAsync();

            ViewBag.Usuarios = new SelectList(usuarios, "Id", "Nome");
            ViewBag.TiposPausa = new SelectList(tiposPausa, "Id", "Nome");
        }
    }
}