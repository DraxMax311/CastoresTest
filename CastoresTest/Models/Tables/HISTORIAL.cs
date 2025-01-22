using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace CastoresTest.Models.Tables
{
    [Table("HISTORIAL")]
    public class HISTORIAL
    {
        [Key]
        public long ID_MOVIMIENTO { get; set; }
        public int ID_USUARIO { get; set; }
        public int ID_PRODUCTO { get; set; }
        public byte TIPO_MOVIMIENTO { get; set; }
        public int CANTIDAD { get; set; }
        public DateTime FECHA { get; set; }
    }
}
