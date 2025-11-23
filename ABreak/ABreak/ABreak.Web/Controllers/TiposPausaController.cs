using ABreak.Application.Interfaces;
using ABreak.Application.ViewModels;
using ABreak.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ABreak.Web.Controllers
{
    public class TiposPausaController : Controller
    {
        private readonly ITipoPausaService _tipoPausaService;

        public TiposPausaController(ITipoPausaService tipoPausaService)
        {
            _tipoPausaService = tipoPausaService;
        }
         
        public async Task<IActionResult> Index()
        {
            var tiposPausa = await _tipoPausaService.GetAllTiposPausaAsync();
            return View(tiposPausa);
        }
         
        public async Task<IActionResult> Details(int id)
        {
            var tipoPausa = await _tipoPausaService.GetTipoPausaByIdAsync(id);
            if (tipoPausa == null)
            {
                return NotFound();
            }
            return View(tipoPausa);
        }
         
        public IActionResult Create()
        {
            return View();
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TipoPausaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tipoPausaDto = new TipoPausaDTO
                {
                    Nome = model.Nome,
                    Descricao = model.Descricao,
                    Icone = model.Icone,
                    DuracaoRecomendadaMinutos = model.DuracaoRecomendadaMinutos,
                    Ativo = true
                };

                await _tipoPausaService.CreateTipoPausaAsync(tipoPausaDto);
                TempData["SuccessMessage"] = "Tipo de pausa criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
         
        public async Task<IActionResult> Edit(int id)
        {
            var tipoPausa = await _tipoPausaService.GetTipoPausaByIdAsync(id);
            if (tipoPausa == null)
            {
                return NotFound();
            }

            var model = new TipoPausaViewModel
            {
                Id = tipoPausa.Id,
                Nome = tipoPausa.Nome,
                Descricao = tipoPausa.Descricao,
                Icone = tipoPausa.Icone,
                DuracaoRecomendadaMinutos = tipoPausa.DuracaoRecomendadaMinutos,
                Ativo = tipoPausa.Ativo
            };

            return View(model);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TipoPausaViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var tipoPausaDto = new TipoPausaDTO
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    Descricao = model.Descricao,
                    Icone = model.Icone,
                    DuracaoRecomendadaMinutos = model.DuracaoRecomendadaMinutos,
                    Ativo = model.Ativo
                };

                await _tipoPausaService.UpdateTipoPausaAsync(tipoPausaDto);
                TempData["SuccessMessage"] = "Tipo de pausa atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
         
        public async Task<IActionResult> Delete(int id)
        {
            var tipoPausa = await _tipoPausaService.GetTipoPausaByIdAsync(id);
            if (tipoPausa == null)
            {
                return NotFound();
            }
            return View(tipoPausa);
        }
         
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _tipoPausaService.DeleteTipoPausaAsync(id);
            TempData["SuccessMessage"] = "Tipo de pausa excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }
    }
}