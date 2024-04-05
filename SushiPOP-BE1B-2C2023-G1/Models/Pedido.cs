using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiPOP_BE1B_2C2023_G1.Models
{
    [Table("T_PEDIDO")]
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nro de pedido")]
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int NroPedido { get; set; }

        [Display(Name = "Fecha de compra")]
        public DateTime? FechaCompra { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public decimal Subtotal { get; set; }

        [Display(Name = "Gasto de envio")]
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public decimal GastoEnvio { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public decimal Total { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int Estado { get; set; } = 1;

        //Relaciones
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int CarritoId { get; set; }
        public Carrito? Carrito { get; set;}

        //Relaciones
        public virtual Reclamo? Reclamo { get; set; }
    }
}
