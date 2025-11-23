using ABreak.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ABreak.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPausaService _pausaService;
        private readonly IUsuarioService _usuarioService;

        public HomeController(IPausaService pausaService, IUsuarioService usuarioService)
        {
            _pausaService = pausaService;
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            var pausas = await _pausaService.GetAllPausasAsync();
            var usuarios = await _usuarioService.GetAllUsuariosAsync();

            ViewBag.TotalPausas = pausas.Count();
            ViewBag.TotalUsuarios = usuarios.Count();
            ViewBag.PausasHoje = pausas.Where(p => p.DataHoraInicio.Date == DateTime.Today).Count();

            return View();
        }
    }
}