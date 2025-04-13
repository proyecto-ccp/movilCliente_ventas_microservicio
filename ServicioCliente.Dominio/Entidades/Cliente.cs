using System.ComponentModel.DataAnnotations.Schema;

namespace Clientes.Dominio.Entidades
{
    [Table("tbl_cliente")]
    public class Cliente : EntidadBase
    {
        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("apellido")]
        public string Apellido { get; set; }

        [Column("tipodocumento")]
        public string TipoDocumento { get; set; }

        [Column("documento")]
        public string Documento { get; set; }

        [Column("direccion")]
        public string Direccion { get; set; }

        [Column("telefono")]
        public string Telefono { get; set; }

        [Column("email")]
        public string Email { get; set; }
    }

}
