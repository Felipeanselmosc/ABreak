using ABreak.Application.Interfaces;
using ABreak.Application.ViewModels;
using ABreak.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ABreak.Web.Controllers
{
    public class PausasController : Controller
    {
        private readonly IPausaService _pausaService;
        private readonly IUsuarioService _usuarioService;
        private readonly ITipoPausaService _tipoPausaService;

        public PausasController(
            IPausaService pausaService,
            IUsuarioService usuarioService,
            ITipoPausaService tipoPausaService)
        {
            _pausaService = pausaService;
            _usuarioService = usuarioService;
            _tipoPausaService = tipoPausaService;
        }
         
        public async Task<IActionResult> Index()
        {
            var pausas = await _pausaService.GetAllPausasAsync();
            return View(pausas);
        }
         
        public async Task<IActionResult> Details(int id)
        {
            var pausa = await _pausaService.GetPausaByIdAsync(id);
            if (pausa == null)
            {
                return NotFound();
            }
            return View(pausa);
        }
         
        public async Task<IActionResult> Create()
        {
            await CarregarSelectLists();
            var model = new PausaViewModel
            {
                DataHoraInicio = DateTime.Now,
                DuracaoMinutos = 5,
                Completada = false
            };
            return View(model);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PausaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var pausaDto = new PausaDTO
                {
                    UsuarioId = model.UsuarioId,
                    TipoPausaId = model.TipoPausaId,
                    DataHoraInicio = model.DataHoraInicio,
                    DataHoraFim = model.DataHoraFim,
                    DuracaoMinutos = model.DuracaoMinutos,
                    Completada = model.Completada,
                    Observacao = model.Observacao
                };

                await _pausaService.CreatePausaAsync(pausaDto);
                TempData["SuccessMessage"] = "Pausa registrada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            await CarregarSelectLists();
            return View(model);
        }
         
        public async Task<IActionResult> Edit(int id)
        {
            var pausa = await _pausaService.GetPausaByIdAsync(id);
            if (pausa == null)
            {
                return NotFound();
            }

            var model = new PausaViewModel
            {
                Id = pausa.Id,
                UsuarioId = pausa.UsuarioId,
                TipoPausaId = pausa.TipoPausaId,
                DataHoraInicio = pausa.DataHoraInicio,
                DataHoraFim = pausa.DataHoraFim,
                DuracaoMinutos = pausa.DuracaoMinutos,
                Completada = pausa.Completada,
                Observacao = pausa.Observacao
            };

            await CarregarSelectLists();
            return View(model);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PausaViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var pausaDto = new PausaDTO
                {
                    Id = model.Id,
                    UsuarioId = model.UsuarioId,
                    TipoPausaId = model.TipoPausaId,
                    DataHoraInicio = model.DataHoraInicio,
                    DataHoraFim = model.DataHoraFim,
                    DuracaoMinutos = model.DuracaoMinutos,
                    Completada = model.Completada,
                    Observacao = model.Observacao
                };

                await _pausaService.UpdatePausaAsync(pausaDto);
                TempData["SuccessMessage"] = "Pausa atualizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }

            await CarregarSelectLists();
            return View(model);
        }
         
        public async Task<IActionResult> Delete(int id)
        {
            var pausa = await _pausaService.GetPausaByIdAsync(id);
            if (pausa == null)
            {
                return NotFound();
            }
            return View(pausa);
        }
         
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _pausaService.DeletePausaAsync(id);
            TempData["SuccessMessage"] = "Pausa excluída com sucesso!";
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