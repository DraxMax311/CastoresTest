using CastoresTest.Models.Tables;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CastoresTest.Models
{
    public class DBContext : IdentityDbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<ROLES_USUARIOS> RolesDB { get; set; }
        public DbSet<USUARIOS> UsuariosDB { get; set; }
        public DbSet<PRODUCTOS> ProductosDB { get; set; }
        public DbSet<HISTORIAL> MovimientosDB { get; set; }
    }
}
