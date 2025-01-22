using CastoresTest.Models;
using CastoresTest.Models.Tables;
using CastoresTest.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CastoresTest.Controllers
{
    public class AddProductsController : Controller
    {
        private readonly DBContext dBContext;

        public AddProductsController(DBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public IActionResult Index()
        {
            var productos = dBContext.ProductosDB.ToList();
            return View(productos);
        }

        public async Task<IActionResult> ListadoProductos()
        {
            return View();
        }
        public async Task<IActionResult> AddProductForm()
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
                    return RedirectToAction("Index", "Home");
                else
                {
                    return View();
                }
            }
        }
        public async Task<IActionResult> EditProductForm(int ID_PRODUCTO)
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
                    return RedirectToAction("Index", "Home");
                else
                {
                    PRODUCTOS producto = dBContext.ProductosDB.Find(ID_PRODUCTO);
                    return View(producto);
                }
            }
        }
        [HttpPost]
        public IActionResult AddProduct(PRODUCTOS product)
        {
            if (ModelState.IsValid)
            {
                // Save the product to the database
                dBContext.ProductosDB.Add(product);
                dBContext.SaveChanges();
                return RedirectToAction("Index", "AddProducts");
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        public async  Task<IActionResult> UpdateProduct(PRODUCTOS product)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = dBContext.ProductosDB.FirstOrDefault(p => p.ID_PRODUCTO == product.ID_PRODUCTO);
                if (existingProduct != null)
                {
                    await writeHistory(product.ID_PRODUCTO, product.CANTIDAD);

                    existingProduct.NOMBRE = product.NOMBRE;
                    existingProduct.CANTIDAD = product.CANTIDAD;
                    existingProduct.ACTIVO = product.ACTIVO;


                    dBContext.SaveChanges();
                    return RedirectToAction("Index"); // Redirect to the product list or another page
                }

                return NotFound(); // Product not found in the database
            }

            return RedirectToAction("Error", "Home"); // Return the view with the product model for error handling
        }
        private async Task<string> writeHistory(int id, int quantity)
        {
            string user = (string)TempData["UsuarioActual"];
            TempData["UsuarioActual"] = user;
            HISTORIAL nuevoMovimiento = new HISTORIAL();
            nuevoMovimiento.ID_USUARIO = int.Parse(user.Split("-")[1]);
            nuevoMovimiento.ID_PRODUCTO = id;
            nuevoMovimiento.TIPO_MOVIMIENTO = 1;
            PRODUCTOS oldProduct = await dBContext.ProductosDB.FindAsync(id);
            int difference = quantity - oldProduct.CANTIDAD;
            nuevoMovimiento.CANTIDAD = difference;
            nuevoMovimiento.FECHA = DateTime.Now;
            await dBContext.MovimientosDB.AddAsync(nuevoMovimiento);
            await dBContext.SaveChangesAsync();
            return "";
        }

    }
}
