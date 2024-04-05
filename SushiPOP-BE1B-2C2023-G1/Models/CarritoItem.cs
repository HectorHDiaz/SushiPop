using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SushiPOP_BE1B_2C2023_G1.Models
{
    [Table("T_CARRITO_ITEM")]
    public class CarritoItem
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Precio unitario con descuento")]
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public decimal PrecioUnitarioConDescuento { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int Cantidad { get; set; }

        //Relaciones
        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int CarritoId { get; set; }
        [ForeignKey("CarritoId")]

        public Carrito? Carrito { get; set; }

        [Required(ErrorMessage = ErrorViewModel.CampoRequerido)]
        public int ProductoId { get; set; }
        [ForeignKey("ProductoId")]

        public Producto? Producto { get; set; }

    }
}
