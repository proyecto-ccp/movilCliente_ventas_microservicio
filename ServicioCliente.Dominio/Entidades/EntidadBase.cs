using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Clientes.Dominio.Entidades
{
    public class EntidadBase
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("fechacreacion", TypeName = "timestamp(6)")]
        public DateTime FechaCreacion { get; set; }

        [Column("fechaactualizacion", TypeName = "timestamp(6)")]
        public DateTime? FechaActualizacion { get; set; }
    }
}
