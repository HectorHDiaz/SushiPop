using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiPOP_BE1B_2C2023_G1.Models
{
    [Table("T_CARRITO")]
    public class Carrito
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public bool Procesado { get; set; } = false;

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public bool Cancelado { get; set; } = false;

        //Relaciones
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]

        public Cliente? Cliente { get; set; }

        //Relaciones
        public virtual Pedido? Pedido { get; set; }

        public ICollection<CarritoItem>? Items { get; set; }

    }
}
