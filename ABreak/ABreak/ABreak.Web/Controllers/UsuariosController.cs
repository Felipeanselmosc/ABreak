using ABreak.Application.Interfaces;
using ABreak.Application.ViewModels;
using ABreak.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ABreak.Web.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
         
        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            return View(usuarios);
        }
         
        public async Task<IActionResult> Details(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }
         
        public IActionResult Create()
        {
            return View();
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuarioDto = new UsuarioDTO
                {
                    Nome = model.Nome,
                    Email = model.Email,
                    Ativo = true
                };

                await _usuarioService.CreateUsuarioAsync(usuarioDto);
                TempData["SuccessMessage"] = "Usuário criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
         
        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var model = new UsuarioViewModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                DataCadastro = usuario.DataCadastro,
                Ativo = usuario.Ativo
            };

            return View(model);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsuarioViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var usuarioDto = new UsuarioDTO
                {
                    Id = model.Id,
                    Nome = model.Nome,
                    Email = model.Email,
                    Ativo = model.Ativo
                };

                await _usuarioService.UpdateUsuarioAsync(usuarioDto);
                TempData["SuccessMessage"] = "Usuário atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
         
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }
         
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _usuarioService.DeleteUsuarioAsync(id);
            TempData["SuccessMessage"] = "Usuário excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }
    }
}