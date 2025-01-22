using CastoresTest.Models;
using CastoresTest.Models.Tables;
using Microsoft.AspNetCore.Mvc;

namespace CastoresTest.Controllers
{
    public class RemoveProductsController : Controller
    {
        private readonly DBContext dBContext;

        public RemoveProductsController(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public IActionResult Index()
        {
            string user = (string)TempData["UsuarioActual"];
            TempData["UsuarioActual"] = user;
            int usuario = int.Parse(user.Split("-")[1]);

            var currentUser = dBContext.UsuariosDB.Find(usuario);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                ROLES_USUARIOS rolActual = dBContext.RolesDB.Find(currentUser.ID_ROL);
                if (!rolActual.NOMBRE_ROL.Equals("ADMIN"))
                {
                    var productos = dBContext.ProductosDB.ToList();
                    return View(productos);
                }
                else
                    return RedirectToAction("Index", "Home");
            }
        }
        public async Task<IActionResult> ListadoProductos()
        {
            return View();
        }
        public async Task<IActionResult> SubtractProductForm(int ID_PRODUCTO)
        {
            string user = (string)TempData["UsuarioActual"];
            TempData["UsuarioActual"] = user;
            int usuario = int.Parse(user.Split("-")[1]);

            var currentUser = dBContext.UsuariosDB.Find(usuario);
            if (currentUser == null)
            {
                return RedirectToAction("Login", "Users");
            }
            else
            {
                ROLES_USUARIOS rolActual = dBContext.RolesDB.Find(currentUser.ID_ROL);
                if (rolActual.NOMBRE_ROL.Equals("ADMIN"))
                    return RedirectToAction("Index", "Home");
                else
                {
                    PRODUCTOS producto = dBContext.ProductosDB.Find(ID_PRODUCTO);
                    return View(producto);
                }
            }

        }

        [HttpPost]
        public async Task<IActionResult> SubstractProducts(PRODUCTOS product)
        {
            var existingProduct = dBContext.ProductosDB.FirstOrDefault(p => p.ID_PRODUCTO == product.ID_PRODUCTO);
            if (existingProduct != null)
            {
                await writeHistory(product.ID_PRODUCTO, product.CANTIDAD);
                existingProduct.CANTIDAD = product.CANTIDAD;
                dBContext.SaveChanges();
                return RedirectToAction("Index"); // Redirect to the product list or another page
            }

            return NotFound(); // Product not found in the database
        }

        private async Task<string> writeHistory(int id, int quantity)
        {
            string user = (string)TempData["UsuarioActual"];
            TempData["UsuarioActual"] = user;
            HISTORIAL nuevoMovimiento = new HISTORIAL();
            nuevoMovimiento.ID_USUARIO = int.Parse(user.Split("-")[1]);
            nuevoMovimiento.ID_PRODUCTO = id;
            nuevoMovimiento.TIPO_MOVIMIENTO = 2;
            PRODUCTOS oldProduct = await dBContext.ProductosDB.FindAsync(id);
            int difference = oldProduct.CANTIDAD - quantity;
            nuevoMovimiento.CANTIDAD = difference;
            nuevoMovimiento.FECHA = DateTime.Now;
            await dBContext.MovimientosDB.AddAsync(nuevoMovimiento);
            await dBContext.SaveChangesAsync();
            return "";
        }
    }
}
