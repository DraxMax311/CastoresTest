using CastoresTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CastoresTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly DBContext _dbContext;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, DBContext dbContext)
        {
            _logger = logger;
            _configuration = configuration;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioActual = TempData["UsuarioActual"];
            TempData["UsuarioActual"] = usuarioActual;
            if (usuarioActual == null)
                return RedirectToAction("login", "Users");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
