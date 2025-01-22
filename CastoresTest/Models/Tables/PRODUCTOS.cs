using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CastoresTest.Models.Tables
{
    [Table("PRODUCTOS")]
    public class PRODUCTOS
    {
        [Key]
        public int ID_PRODUCTO { get; set; }
        public string? NOMBRE { get; set; }
        public int CANTIDAD { get; set; }
        public bool ACTIVO { get; set; }
    }
}
