using CastoresTest.Models;
using CastoresTest.Models.Tables;
using CastoresTest.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CastoresTest.Controllers
{
    public class HistoryController : Controller
    {
        private readonly DBContext dBContext;

        public HistoryController(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public IActionResult Index()
        {
            string usuarioActual = (string)TempData["UsuarioActual"];
            TempData["UsuarioActual"] = usuarioActual;
            if (usuarioActual == null)
                return RedirectToAction("Index", "Home");
            else
            {
                string id = usuarioActual.Split("-")[1];
                var usuario = dBContext.UsuariosDB.First(x => x.ID_USUARIO == int.Parse(id));
                var ROL = dBContext.RolesDB.FirstOrDefault(x => x.ID_ROL == usuario.ID_ROL);
                if (ROL == null || ROL.NOMBRE_ROL != "ADMIN")
                    return RedirectToAction("Index", "Home");
                else
                {
                    var movimientos = dBContext.MovimientosDB.ToList();
                    return View(movimientos);
                }
            }
        }
        [HttpGet]
        public IActionResult ListadoMovimientos(byte? filtro)
        {
            try
            {

                List<HISTORIAL> movimientos;
                if (filtro != null)
                    movimientos = dBContext.MovimientosDB.Where(x => x.TIPO_MOVIMIENTO == (byte)filtro).ToList();
                else
                    movimientos = dBContext.MovimientosDB.ToList();
                return PartialView(movimientos);
            }
            catch (Exception ex)
            {
                return BadRequest();
        }
        }


    }
}
