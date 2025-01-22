using CastoresTest.Models;
using CastoresTest.Models.Tables;
using CastoresTest.Models.ViewModels;
using CastoresTest.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CastoresTest.Controllers
{
    public class UsersController : Controller
    {
        private readonly DBContext dBContext;

        public UsersController(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var resultado = await dBContext.UsuariosDB.FirstOrDefaultAsync(x=>x.CORREO.Equals(model.Email) && x.CONTRASENA.Equals(model.Password) && x.ESTATUS != false);
            if (resultado != null)
            {
                ROLES_USUARIOS rol = dBContext.RolesDB.Find(resultado.ID_ROL);
                TempData["UsuarioActual"] = resultado.NOMBRE.ToString() + "-" + resultado.ID_ROL.ToString() + "-" + rol.NOMBRE_ROL.ToString() + "-" + resultado.CORREO.ToString() + "-" + resultado.ID_USUARIO.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "El nombre del usuario o password son incorrectos.");
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            TempData["UsuarioActual"] = null;
            return RedirectToAction("Login", "Users");
        }

        [HttpGet]
        public async Task<IActionResult> Listado(string mensaje = null)
        {
            var usuarios = await dBContext.Users.Select(x => new UsuarioViewModel
            {
                Email = x.Email
            }).ToListAsync();

            var modelo = new UsuariosListadoViewModel();
            modelo.Usuarios = usuarios;
            modelo.Mensaje = mensaje;
            return View(modelo);

        }

        [HttpPost]
        public async Task<IActionResult> HacerAdmin(string email)
        {
            var usuario = await dBContext.UsuariosDB.Where(x => x.CORREO == email).FirstOrDefaultAsync();
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.ID_ROL = 1;
            dBContext.UsuariosDB.Update(usuario);
            await dBContext.SaveChangesAsync();

            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> RemoverAdmin(string email)
        {
            var usuario = await dBContext.UsuariosDB.Where(x => x.CORREO == email).FirstOrDefaultAsync();
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.ID_ROL = 2;
            dBContext.UsuariosDB.Update(usuario);
            await dBContext.SaveChangesAsync();

            return RedirectToAction("Login", "Home");
        }
    }
}
