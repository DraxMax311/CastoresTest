using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CastoresTest.Models.Tables
{
    [Table("USUARIOS")]
    public class USUARIOS
    {
        [Key]
        public int ID_USUARIO { get; set; }
        public string? NOMBRE { get; set; }
        public string? CORREO { get; set; }
        public string? CONTRASENA { get; set; }
        public int ID_ROL { get; set; }
        public bool ESTATUS { get; set; }
    }
}
