using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CastoresTest.Models.Tables
{
    [Table("ROLES_USUARIOS")]
    public class ROLES_USUARIOS
    {
        [Key]
        public int ID_ROL { get; set; }
        public string? NOMBRE_ROL { get; set; }
    }
}
