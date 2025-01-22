using System.ComponentModel.DataAnnotations;

namespace CastoresTest.Models.ViewModels
{
    public class USUARIOSCONROLES
    {
        public int ID_USUARIO { get; set; }
        public string? NOMBRE { get; set; }
        public string? CORREO { get; set; }
        public string? CONTRASENA { get; set; }
        public string ROL { get; set; }
        public bool ESTATUS { get; set; }
    }
}
